using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Wechat.Pay.Core.Interface;
using Wechat.Pay.Core.Request;
using Wechat.Pay.Core.Response;

namespace Wechat.Pay.Core
{
    public sealed class WechatClient : IClient
    {

        private readonly WechatConfig config;

        private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { IgnoreNullValues = true };

        public WechatClient(WechatConfig config)
        {
            this.config = config;
        }

        public async Task<T> ExecuteAsync<T>(SDKRequest<T> request) where T : SDKResponse
        {

            using (HttpClient client = new HttpClient(new HttpHandler(config.MerchantId, config.Certificate)))
            {
                using var response = (request.RequestMethod()) switch
                {
                    "GET" => await client.GetAsync(request.RequestUrl()),
                    "POST" => await client.PostAsync(request.RequestUrl(), new StringContent(JsonSerializer.Serialize(request),Encoding.UTF8,"application/json")),
                    _ => throw new ArgumentException("Unsupported HTTP method!"),
                };

                if (response != null)
                {
                    var repContent = await response.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<T>(repContent, jsonSerializerOptions);
                }
            }

            return null;
        }
    }


    internal class HttpHandler : DelegatingHandler
    {
        private readonly string merchantId;
        private readonly X509Certificate2 certificate;

        internal HttpHandler(string merchantId, X509Certificate2 certificate)
        {
            InnerHandler = new HttpClientHandler();
            this.merchantId = merchantId;
            this.certificate = certificate;

        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var auth = await BuildAuthAsync(request);

            request.Headers.Authorization = new AuthenticationHeaderValue("WECHATPAY2-SHA256-RSA2048", auth);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("dotnet", "3.1.8")));

            return await base.SendAsync(request, cancellationToken);
        }

        protected async Task<string> BuildAuthAsync(HttpRequestMessage request)
        {
            string method = request.Method.ToString();
            string body = "";
            if (method == "POST" || method == "PUT" || method == "PATCH")
            {
                var content = request.Content;
                body = await content.ReadAsStringAsync();
            }

            string uri = request.RequestUri.PathAndQuery;
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            string nonce = Guid.NewGuid().ToString("N");

            string message = $"{method}\n{uri}\n{timestamp}\n{nonce}\n{body}\n";

            using (RSA rsa = certificate.GetRSAPrivateKey())
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                message = Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }

            return $"mchid=\"{merchantId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",serial_no=\"{certificate.SerialNumber}\",signature=\"{message}\"";
        }


    }
}
