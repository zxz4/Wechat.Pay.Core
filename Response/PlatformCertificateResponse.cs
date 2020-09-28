using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Wechat.Pay.Core.Response
{

    /// <summary>
    /// 获取平台证书列表
    /// https://wechatpay-api.gitbook.io/wechatpay-api-v3/jie-kou-wen-dang/ping-tai-zheng-shu
    /// </summary>
    public sealed class PlatformCertificateResponse : SDKResponse
    {
        /// <summary>
        /// 证书列表
        /// </summary>
       [JsonPropertyName("data")]
        public IEnumerable<PlatformCertificate> Data { get; set; }
    }

    #region 证书信息
    /// <summary>
    /// 平台响应证书详情
    /// </summary>
    public class PlatformCertificate
    {
        [JsonPropertyName("serial_no")]
        public string SerialNo { get; set; }

        [JsonPropertyName("effective_time")]

        public DateTimeOffset EffectiveTime { get; set; }


        [JsonPropertyName("expire_time ")]
        public DateTimeOffset ExpireTime { get; set; }


        [JsonPropertyName("encrypt_certificate")]
        public CertificateEncryptInfo EncryptInfo { get; set; }
    }

    /// <summary>
    /// 解密信息
    /// </summary>
    public class CertificateEncryptInfo
    {
        [JsonPropertyName("algorithm")]
        public string Algorithm { get; set; }
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        [JsonPropertyName("associated_data")]
        public string AssociatedData { get;set; }

        [JsonPropertyName("ciphertext")]
        public string CipherText { get; set; }
    }
    #endregion

}
