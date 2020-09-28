using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Text.Json.Serialization;

namespace Wechat.Pay.Core.Response
{

    /// <summary>
    /// 响应基类
    /// </summary>
    public abstract class SDKResponse
    {


        #region 通用错误信息

        /// <summary>
        /// 详细错误码
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = "200";

        /// <summary>
        /// 错误描述
        /// 使用易理解的文字表示错误的原因。
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = "OK";


        /// <summary>
        /// 错误详情
        /// </summary>
        [JsonPropertyName("detail")]
        public ErrorInfo Detail { get; set; }

        #endregion

    }


    /// <summary>
    /// 具体错误原因
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// 指示错误参数的位置。
        /// 当错误参数位于请求body的JSON时，填写指向参数的JSON 
        /// </summary>
        [JsonPropertyName("field")]
        public string Field { get; set; }

        /// <summary>
        /// 错误的值
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// 具体错误原因
        /// </summary>
        [JsonPropertyName("issue")]
        public string Issue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("location")]
        public string Location { get; set; }
    }
}
