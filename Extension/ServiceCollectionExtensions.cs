using Microsoft.Extensions.DependencyInjection;

namespace Wechat.Pay.Core.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWechatPay(this IServiceCollection services,WechatOptions options)
        {
            services.AddScoped<Interface.IWeChatPayClient, WechatPayClient>();

            services.AddSingleton(options);
        }
    }
}
