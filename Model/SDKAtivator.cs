using System.Text.Json.Serialization;

namespace Wechat.Pay.Core.Model
{
    /// <summary>
    /// 调起
    /// </summary>
    public class SDKAtivator
    {

        [JsonPropertyName("appId")]
        public string AppId { get; set; }

        [JsonPropertyName("timeStamp")]
        public long TimeStamp { get;  set; } 

        [JsonPropertyName("nonceStr")]
        public string NonceStr { get; set; } 

        [JsonPropertyName("package")]
        public string Package { get; set; }

        [JsonPropertyName("signType")]
        public string SignType { get; private set; } = "RSA";

        [JsonPropertyName("paySign")]
        public string PaySign { get; set; }

    }
}
