﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Wechat.Pay.Core.Interface;
using Wechat.Pay.Core.Response;

namespace Wechat.Pay.Core.Request
{

    /// <summary>
    /// 获取平台证书列表
    /// https://wechatpay-api.gitbook.io/wechatpay-api-v3/jie-kou-wen-dang/ping-tai-zheng-shu
    /// </summary>
    public class PlatformCertificate : SDKRequest<PlatformCertificateResponse>
    {
        public override string RequestUrl()
        {
            return "https://api.mch.weixin.qq.com/v3/certificates";
        }

        public override string RequestMethod()
        {
            return "GET";
        }

    }
}
