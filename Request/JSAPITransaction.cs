using System;
using System.Text.Json.Serialization;
using Wechat.Pay.Core.Response;

namespace Wechat.Pay.Core.Request
{

    /// <summary>
    /// JSAPI/小程序下单API
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/wxpay/pay/transactions/chapter3_2.shtml
    /// </summary>
    public class JSAPITransaction : SDKRequest<JSAPITransactionResponse>
    {
        /// <summary>
        /// 公众号ID
        /// 直连商户申请的公众号或移动应用appid。
        /// 示例值：wxd678efh567hg6787
        /// </summary>
        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 直连商户号
        /// 直连商户的商户号，由微信支付生成并下发。
        /// 示例值：1230000109
        /// </summary>
        [JsonPropertyName("mchid")]
        public string MchId { get; set; }

        /// <summary>
        /// 商品描述
        /// 示例值：Image形象店-深圳腾大-QQ公仔
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// 商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一，详见【商户订单号】。
        /// 特殊规则：最小字符长度为6
        /// 示例值：1217752501201407033233368018
        /// </summary>
        [JsonPropertyName("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 订单失效时间
        /// 示例值：2018-06-08T10:34:56+08:00
        /// </summary>
        [JsonPropertyName("time_expire")]
        public DateTimeOffset? TimeExpire { get; set; } 

        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用
        /// 示例值：自定义数据
        /// </summary>
        [JsonPropertyName("attach")]
        public string Attach { get; set; } = null;

        /// <summary>
        /// 通知地址
        /// 通知URL必须为直接可访问的URL，不允许携带查询串。
        /// 示例值：https://www.weixin.qq.com/wxpay/pay.php
        /// </summary>
        [JsonPropertyName("notify_url")]
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 订单优惠标记
        /// 示例值：WXG
        /// </summary>
        [JsonPropertyName("goods_tag")]
        public string GoodsTag { get; set; } = null;

        /// <summary>
        /// 订单金额
        /// </summary>
        [JsonPropertyName("amount")]
        public Amount Amount { get; set; }

        /// <summary>
        /// 支付者
        /// </summary>
        [JsonPropertyName("payer")]
        public PayerInfo Payer { get; set; }

        public override string RequestMethod()
        {
            return "POST";
        }

        public override string RequestUrl()
        {
            return "https://api.mch.weixin.qq.com/v3/pay/transactions/jsapi";
        }
    }

    /// <summary>
    /// 订单金额信息
    /// </summary>
    public class Amount
    {
        /// <summary>
        /// 订单总金额，单位为分。
        /// 示例值：100
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
        /// 货币类型
        /// CNY：人民币，境内商户号仅支持人民币。
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; private set; } = "CNY";
    }

    /// <summary>
    /// 支付者信息
    /// </summary>
    public class PayerInfo
    {
        /// <summary>
        /// 用户在直连商户appid下的唯一标识。
        /// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
        /// </summary>
        public string OpenId { get; set; }
    }
}
