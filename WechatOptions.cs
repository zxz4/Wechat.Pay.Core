using System;
using System.Security.Cryptography.X509Certificates;

namespace Wechat.Pay.Core
{
    public class WechatOptions
    {

        /// <summary>
        /// 初始化支付设置(直连方式)
        /// </summary>
        /// <param name="merchantId">商户号</param>
        /// <param name="certificatePath">p12证书所在路径</param>
        /// <param name="apiSecret">apiv3密钥</param>

        public WechatOptions(string appId, string merchantId, string certificatePath, string apiSecret)
        {

            if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(merchantId) || string.IsNullOrWhiteSpace(certificatePath) || string.IsNullOrWhiteSpace(apiSecret)) 
            {
                throw new ArgumentException();
            }

            AppId = appId;

            MerchantId = merchantId;

            APISecret = apiSecret;

            Certificate = new X509Certificate2(certificatePath, merchantId);
        }


        /// <summary>
        /// 服务商公众号
        /// </summary>
        public string AppId { get; set; }


        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantId { get; private set; }


        /// <summary>
        /// 证书
        /// </summary>
        internal X509Certificate2 Certificate { get; private set; }


        /// <summary>
        /// APIV3密钥
        /// </summary>
        internal string APISecret { get; private set; }

    }
}
