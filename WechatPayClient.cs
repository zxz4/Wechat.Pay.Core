using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Wechat.Pay.Core.Extension;
using Wechat.Pay.Core.Infrastructure;
using Wechat.Pay.Core.Interface;
using Wechat.Pay.Core.Model;
using Wechat.Pay.Core.Notify;
using Wechat.Pay.Core.Request;
using Wechat.Pay.Core.Response;

namespace Wechat.Pay.Core
{
    public sealed class WechatPayClient : IWeChatPayClient
    {


        public async Task<T> ExecuteRequestAsync<T>(IWechatPayRequestSDK<T> request, WechatOptions options) where T : WechatPayRequestSDKResponse
        {
            using HttpClient client = new HttpClient();

            var (headers, responseContent, sdkResponse) = await client.ExecuteResponseAsync(request, options);

            if (request.ValidateResponse())
            {
                try
                {
                    await ValidateSignAsync(headers, responseContent, options);
                }
                catch (Exception ex)
                {
                    sdkResponse.Code = "400";
                    sdkResponse.Message = ex.Message;
                }
            }

            return sdkResponse;
        }

        public async Task<SDKAtivator> ExecuteSDKAsync(IWechatPaySDK ativator, WechatOptions options)
        {
            var resp = new SDKAtivator
            {

                AppId = options.AppId,

                TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds(),

                NonceStr = Guid.NewGuid().ToString("N"),

                Package = ativator.GetPackage()

            };

            string message = $"{resp.AppId}\n{resp.TimeStamp}\n{resp.NonceStr}\n{resp.Package}\n";

            using (RSA rsa = options.Certificate.GetRSAPrivateKey())
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                resp.PaySign = Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }

            return await Task.FromResult(resp);
        }


        public async Task<T> ExecuteNofityAsync<T>(WechatPayHeader header, WechatNotificationPayload<T> notification, WechatOptions options) where T: WechatPayNotification 
        {

            try
            {
                await ValidateSignAsync(header, JsonSerializer.Serialize(notification,notification.GetType()), options);

                byte[] decryptRawContent;

                switch (notification?.EncryptInfo.Algorithm)
                {
                    case "AEAD_AES_256_GCM":
                        {
                            decryptRawContent = notification.EncryptInfo.Decrypt(options.APISecret);
                        }
                        break;
                    default:
                        throw new Exception("Unsupported Encrypt Algorithm!");

                }

                notification.WechatPayNotification = JsonSerializer.Deserialize<T>(decryptRawContent);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return notification.WechatPayNotification;
        }

        /// <summary>
        /// 验证应答签名
        /// </summary>
        /// <param name="header"></param>
        /// <param name="responseContent"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private async Task ValidateSignAsync(WechatPayHeader header, string responseContent, WechatOptions options)
        {

            if (string.IsNullOrWhiteSpace(header?.Nonce) || string.IsNullOrWhiteSpace(header.SerialNo) || string.IsNullOrWhiteSpace(header.Signature) || string.IsNullOrWhiteSpace(header.TimeStamp))

                throw new ArgumentException();

            var certificate = await GetPlatformCertificateAsync(header.SerialNo, options);

            if (certificate == null)
            {
                throw new Exception("Can't Get PLATFORM CERTIFICATE");
            }

            string message = $"{header.TimeStamp}\n{header.Nonce}\n{responseContent}\n";

            using var rsa = certificate.GetRSAPublicKey();

            if (!rsa.VerifyData(Encoding.UTF8.GetBytes(message), Convert.FromBase64String(header.Signature), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1))

                throw new Exception("Validate Sinature Failed!");
        }


        /// <summary>
        /// 获取平台证书
        /// </summary>
        /// <param name="serialNo"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private async Task<X509Certificate2> GetPlatformCertificateAsync(string serialNo, WechatOptions options)
        {

            var certificate = PlateformCertificateManager.CertificateManager[serialNo];

            if (certificate != null)
            {
                return certificate;
            }

            var response = await ExecuteRequestAsync(new WechatPayPlatformCertificateRequest(), options);

            if (response.Data.Equals(null) || response.Data.Length == 0)
            {
                throw new Exception("GET PLATFORM CERTIFICATE FAILED!");
            }

            foreach (var item in response.Data)
            {
                switch (item.EncryptInfo.Algorithm)
                {
                    case "AEAD_AES_256_GCM":
                        {
                            PlateformCertificateManager.CertificateManager[serialNo] =  new X509Certificate2(item.EncryptInfo.Decrypt(options.APISecret));
                        }
                        break;
                    default:
                        throw new Exception("Unsupported Encrypt Algorithm!");
                }
            }
            return PlateformCertificateManager.CertificateManager[serialNo];

        }
    }

}
