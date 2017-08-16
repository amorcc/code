using cc.common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cc.utility;
using Newtonsoft.Json;
using cc.common.core;

namespace cc.webapi.Controllers
{
    public class AlipayController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Alipay([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCodes = jsonPara.GetValueExt<string>("OrderCodes", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                string openid = jsonPara.GetValueExt<string>("Openid", "");
                int payType = jsonPara.GetValueExt<int>("PayType", 0);
                string rn = jsonPara.GetValueExt<string>("rn", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IAlipay alipayBC = new cc.services.Alipay();
                var result = alipayBC.Alipay(loginUser, rn, orderCodes, payType);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };

            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }
    }
}
