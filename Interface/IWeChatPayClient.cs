using System.Threading.Tasks;
using Wechat.Pay.Core.Model;
using Wechat.Pay.Core.Notify;
using Wechat.Pay.Core.Response;

namespace Wechat.Pay.Core.Interface
{
    public interface IWeChatPayClient
    {

        /// <summary>
        /// 执行HTTP Request请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<T> ExecuteRequestAsync<T>(IWechatPayRequestSDK<T> request, WechatOptions options) where T : WechatPayRequestSDKResponse;



        /// <summary>
        /// 执行SDK
        /// </summary>
        /// <param name="ativator"></param>
        /// <param name="options"></param>
        /// <returns></returns>

        Task <SDKAtivator> ExecuteSDKAsync(IWechatPaySDK ativator, WechatOptions options);



        /// <summary>
        /// 处理通知对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="header"></param>
        /// <param name="notification"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<T> ExecuteNofityAsync<T>(WechatPayHeader header, WechatNotificationPayload<T> notification, WechatOptions options) where T : WechatPayNotification;
    }
}
