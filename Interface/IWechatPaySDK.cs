namespace Wechat.Pay.Core.Interface
{
    public interface IWechatPaySDK
    {

        /// <summary>
        /// 获取扩展字符串
        /// </summary>
        /// <returns></returns>
        internal string GetPackage();
    }
}
