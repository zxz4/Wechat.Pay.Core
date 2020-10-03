using Wechat.Pay.Core.Response;

namespace Wechat.Pay.Core.Interface
{
    public interface IWechatPayRequestSDK<T> where T : WechatPayRequestSDKResponse
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl();

        /// <summary>
        /// 请求方法
        /// </summary>
        /// <returns></returns>
        public string RequestMethod();


        /// <summary>
        /// 是否验证回调应答签名
        /// </summary>
        /// <returns></returns>
        public  bool ValidateResponse();

    }
}
