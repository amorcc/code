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

    }
}
