using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wechat.Pay.Core.Request;
using Wechat.Pay.Core.Response;

namespace Wechat.Pay.Core.Interface
{
    public interface IClient
    {
        /// <summary>
        /// 执行请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<T> ExecuteAsync<T>(SDKRequest<T> request) where T : SDKResponse;
    }
}
