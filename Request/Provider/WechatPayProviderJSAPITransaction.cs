using System;
using System.Text.Json.Serialization;
using Wechat.Pay.Core.Interface;
using Wechat.Pay.Core.Model;
using Wechat.Pay.Core.Response;

namespace Wechat.Pay.Core.Request.Provider
{


    /// <summary>
    /// JSAPI/小程序下单API（服务商模式）
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/wxpay/pay/transactions/chapter5_2.shtml
    /// </summary>
    public class WechatPayProviderJSAPITransaction : IWechatPayRequestSDK<WechatPayJSAPITransactionResponse>
    {

        /// <summary>
        /// 服务商公众号ID
        /// 服务商申请的公众号或移动应用appid。
        /// 示例值：wx8888888888888888
        /// </summary>
        [JsonPropertyName("sp_appid")]
        public string SPAppId { get; set; }

        /// <summary>
        /// 服务商户号
        /// 服务商户号，由微信支付生成并下发
        /// 示例值：1230000109
        /// </summary>
        [JsonPropertyName("sp_mchid")]
        public string SPMchId { get; set; }


        /// <summary>
        /// 子商户公众号ID
        /// 子商户申请的公众号或移动应用appid。
        /// 示例值：wxd678efh567hg6999
        /// </summary>
        [JsonPropertyName("sub_appid")]
        public string SubAppId { get; set; }


        /// <summary>
        /// 子商户号
        /// 子商户的商户号，有微信支付生成并下发。
        /// 示例值：1900000109
        /// </summary>
        [JsonPropertyName("sub_mchid")]
        public string SubMchId { get; set; }

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


        public  string RequestMethod()
        {
            return "POST";
        }

        public  string RequestUrl()
        {
            return "https://api.mch.weixin.qq.com/v3/pay/partner/transactions/jsapi";
        }

        public bool ValidateResponse()
        {
            return false;
        }
    }
}
