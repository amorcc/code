using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using cc.common.Utility;

namespace cc.common.TokenMng
{
    /// <summary>
    /// webapi权限过滤器
    /// cc  2016-11-19
    /// 对于login函数不进行过滤
    /// </summary>
    public class TokenFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            HttpRequestMessage request1 = actionContext.Request;
            if (request1.Properties.ContainsKey("HTTP_X_FORWARDED_FOR"))
            {
                dynamic ctx = request1.Properties["HTTP_X_FORWARDED_FOR"];
                if (ctx != null)
                {
                    string host = ctx.Request.UserHostAddress;
                    Log(string.Format("HTTP_X_FORWARDED_FOR:{0},action:{1}", host, actionContext.ActionDescriptor.ActionName));
                }
            }
            if (request1.Properties.ContainsKey("MS_HttpContext"))
            {
                dynamic ctx = request1.Properties["MS_HttpContext"];
                if (ctx != null)
                {
                    string host = ctx.Request.UserHostAddress;
                    Log(string.Format("MS_HttpContext:{0},action:{1}", host, actionContext.ActionDescriptor.ActionName));
                }
            }
            if (request1.Properties.ContainsKey("System.ServiceModel.Channels.RemoteEndpointMessageProperty"))
            {
                dynamic remoteEndpoint = request1.Properties["System.ServiceModel.Channels.RemoteEndpointMessageProperty"];
                if (remoteEndpoint != null)
                {
                    string host = remoteEndpoint.Address;
                    Log(string.Format("RemoteEndpointMessageProperty:{0},action:{1}", host, actionContext.ActionDescriptor.ActionName));
                }
            }

            //GetToken方法不需要进行签名验证
            if (TokenNoChk.IsTonkenNoCheck(actionContext.ActionDescriptor.ActionName) == false)
            {
                string jsonParaStr = actionContext.ActionArguments["jsonPara"] == null ? "{}" : actionContext.ActionArguments["jsonPara"].ToString();
                JObject jo = JObject.Parse(jsonParaStr);
                string tokenStr = (string)jo.GetValueExt<string>("token", "");

                if (string.IsNullOrEmpty(tokenStr) || TokenManage.GetUser(tokenStr) == null)
                {
                    JObject rt = new JObject()
                    {
                       {"ResponseID", 1},
                       {"Message","用户登录状态异常，请重新登录!"},
                       {"Data",""},
                    };

                    actionContext.Response = new HttpResponseMessage { Content = new StringContent(rt.ToString(), System.Text.Encoding.UTF8, "application/json") };
                    base.OnActionExecuting(actionContext);
                    return;
                }
            }

            base.OnActionExecuting(actionContext);

        }


        void Log(string logInfo)
        {
            JObject jo = new JObject()
            {
                {"logtype", 1},
                {"userid", 0},
                {"usersn", ""},
                {"roleid", ""},
                {"siteid", 1},
                {"batchid", ""},
                {"ordercode", ""},
                {"description", logInfo},
                {"nodename", ""},
                {"businesscode", ""},
                {"iparas", ""},
                {"oparas", ""},
                {"ip",  ""},
                {"classinfo",  this.GetType().ToString()},
            };

            string info = RemoveEnterChar(jo.ToString());

            log4net.LogManager.GetLogger(this.GetType()).Debug(info);
        }

        /// <summary>
        /// 过滤回车
        /// </summary>
        /// <param name="iStr"></param>
        /// <returns></returns>
        private static string RemoveEnterChar(string iStr)
        {
            if (!string.IsNullOrEmpty(iStr))
            {
                iStr = iStr.Replace("\r", "");
                iStr = iStr.Replace("\n", "");
            }

            return iStr;
        }
    }
}
