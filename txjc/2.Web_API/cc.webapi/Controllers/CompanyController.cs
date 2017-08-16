using cc.common;
using cc.common.core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cc.utility;

namespace cc.webapi.Controllers
{
    public class CompanyController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetCompanyInfo([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.ICompanyMng companyBC = new cc.services.CompanyMng();
                var result = companyBC.GetCompanyInfo(loginUser);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage CreateInviteQRCode([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.ICompanyMng companyBC = new cc.services.CompanyMng();
                var result = companyBC.CreateInviteQRCode(loginUser);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage JoinMe([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string userSN_S = jsonPara.GetValueExt<string>("UserSN_S", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    JObject a = new JObject()
                    {
                        {"IsLogin",0}
                    };
                    cc.common.Utility.ActionResult<JObject> result1 = cc.common.Utility.MyResponse.ToYou<JObject>(a);

                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result1), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.ICompanyMng companyBC = new cc.services.CompanyMng();
                var result = companyBC.JoinMe(loginUser, userSN_S);

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
