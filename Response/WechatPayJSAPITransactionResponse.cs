using System.Text.Json.Serialization;
using Wechat.Pay.Core.Interface;

namespace Wechat.Pay.Core.Response
{
    public class WechatPayJSAPITransactionResponse : WechatPayRequestSDKResponse , IWechatPaySDK
    {

        /// <summary>
        /// 预支付订单ID
        /// </summary>
        [JsonPropertyName("prepay_id")]
        public string PrepayId { get; set; }

        string IWechatPaySDK.GetPackage()
        {
            return $"prepay_id={PrepayId}";
        }
    }
}
