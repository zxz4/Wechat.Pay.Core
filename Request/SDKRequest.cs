using Wechat.Pay.Core.Response;

namespace Wechat.Pay.Core.Request
{
    public abstract class SDKRequest<T> where T : SDKResponse
    {

        /// <summary>
        /// 地址
        /// </summary>
        public abstract string RequestUrl();

        /// <summary>
        /// 请求方法
        /// </summary>
        /// <returns></returns>
        public abstract string RequestMethod();



    }
}
