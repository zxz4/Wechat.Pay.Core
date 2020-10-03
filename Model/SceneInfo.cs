using System.Text.Json.Serialization;

namespace Wechat.Pay.Core.Model
{
    /// <summary>
    /// 场景信息
    /// </summary>
    public class SceneInfo
    {
        /// <summary>
        /// 商户端设备号
        /// </summary>
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }
    }
}
