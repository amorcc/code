using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.log
{
    /// <summary>
    /// 信息传递到哪个API
    /// </summary>
    public enum SiteWebAPI
    {
        UCWebAPI = 1,
        B2bWebAPI = 2,
        PSIWebAPI = 3,
        RCMWebAPI = 4,
        SentinelWebAPI = 5,
    }

    /// <summary>
    /// 定义日志的Action常量
    /// </summary>
    public class LogActionUrl
    {
        public const string LoginLog = "/api/LogMng/LoginLog";
        public const string DefaultLog = "/api/LogMng/BusinessLogicLog";
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 登录日志
        /// </summary>
        LoginLog = 1,
        /// <summary>
        /// 业务流程日志
        /// </summary>
        BusinessLogicLog = 2,
    }

    /// <summary>
    /// 业务流程日志类型
    /// </summary>
    public enum BLLogType
    {
        DEBUG = 1,
        INFO = 2,
        WARN = 3,
        ERROR = 4,
        FATAL = 5,
        OrderLog = 6,
    }

    /// <summary>
    /// 日志请求数据类型
    /// </summary>
    public class RequestData
    {
        public JObject Para;
        public SiteWebAPI SiteWebAPI;
        public string ActionUrl
        {
            get
            {
                string actionUrl = string.Empty;
                switch (this.LogType)
                {
                    case LogType.LoginLog:
                        actionUrl = LogActionUrl.LoginLog;
                        break;
                    default:
                        actionUrl = LogActionUrl.DefaultLog;
                        break;
                }

                return actionUrl;
            }
        }

        public bool IsQueue;
        public LogType LogType;

        public RequestData(SiteWebAPI iSiteWebAPI, LogType iLogType, JObject iPara, bool iIsQueue = false)
        {
            this.SiteWebAPI = iSiteWebAPI;
            this.LogType = iLogType;
            this.Para = iPara;
            this.IsQueue = iIsQueue;
        }

        /// <summary>
        /// 返回调用的webapi的url地址：域名+控制器+action
        /// </summary>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public string GetWebAPIUrl(out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            string url = string.Empty;

            if (string.IsNullOrEmpty(ActionUrl))
            {
                iErrorMsg = "Action为空";
                return url;
            }

            switch (SiteWebAPI)
            {
                case SiteWebAPI.UCWebAPI:
                    url = HX.Utility.Common.App("ucapiurl");
                    break;
                case SiteWebAPI.B2bWebAPI:
                    url = HX.Utility.Common.App("b2bapiurl");
                    break;
                case SiteWebAPI.PSIWebAPI:
                    url = HX.Utility.Common.App("psiapiurl");
                    break;
                case SiteWebAPI.RCMWebAPI:
                    url = HX.Utility.Common.App("rcmapiurl");
                    break;
                case SiteWebAPI.SentinelWebAPI:
                    url = HX.Utility.Common.App("sentineapiurl");
                    break;
                default:
                    break;
            }

            if (string.IsNullOrEmpty(url))
            {
                iErrorMsg = "webapi的Url地址未配置";
                return url;
            }

            url += "/" + ActionUrl;

            return url;
        }
    }
}
