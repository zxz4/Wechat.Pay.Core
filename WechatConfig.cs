using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Wechat.Pay.Core
{
    public class WechatConfig
    {

        /// <summary>
        /// 初始化支付设置
        /// </summary>
        /// <param name="merchantId">商户号</param>
        /// <param name="certificatePath">p12证书所在路径</param>
        /// <param name="apiSecret">apiv3密钥</param>

        public WechatConfig(string merchantId,string certificatePath,string apiSecret)
        {
            MerchantId = merchantId;

            APISecret = apiSecret;

            Certificate = new X509Certificate2(certificatePath,merchantId);
        }


        /// <summary>
        /// 商户号
        /// </summary>
        internal string MerchantId { get; private set; }


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
