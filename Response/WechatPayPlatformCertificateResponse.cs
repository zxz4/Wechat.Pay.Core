using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Wechat.Pay.Core.Response
{

    /// <summary>
    /// 获取平台证书列表
    /// https://wechatpay-api.gitbook.io/wechatpay-api-v3/jie-kou-wen-dang/ping-tai-zheng-shu
    /// </summary>
    public sealed class WechatPayPlatformCertificateResponse : WechatPayRequestSDKResponse
    {
        /// <summary>
        /// 证书列表
        /// </summary>
        [JsonPropertyName("data")]
        public PlatformCertificate[] Data { get; set; }
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
        public EncryptInfo EncryptInfo { get; set; }
    }

    /// <summary>
    /// 解密信息
    /// </summary>
    public class EncryptInfo
    {
        [JsonPropertyName("algorithm")]
        public string Algorithm { get; set; }
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        [JsonPropertyName("associated_data")]
        public string AssociatedData { get; set; }

        [JsonPropertyName("ciphertext")]
        public string CipherText { get; set; }


        /// <summary>
        /// AEAD_AES_256_GCM
        /// https://tools.ietf.org/html/rfc5116#page-15
        /// GCM加密后附加128位TAG形成密文
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        public byte[] Decrypt(string key)
        {
            using var aesGcm = new AesGcm(Encoding.UTF8.GetBytes(key));
            var data = Convert.FromBase64String(CipherText);
            var cipher = data[0..^16];
            var tag = data[^16..];
            var plain = new byte[cipher.Length];
            aesGcm.Decrypt(Encoding.UTF8.GetBytes(Nonce), cipher, tag, plain, Encoding.UTF8.GetBytes(AssociatedData));
            return plain;
        }
    }
    #endregion

}
