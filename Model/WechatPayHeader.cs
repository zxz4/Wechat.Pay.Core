namespace Wechat.Pay.Core.Response
{
    /// <summary>
    /// 应答响应头
    /// </summary>

    public class WechatPayHeader
    {

        /// <summary>
        /// 随机串
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// 签名值
        /// </summary>
        public string Signature { get; set; }


        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 商户API证书序列号
        /// </summary>
        public string SerialNo { get; set; }

    }
}
