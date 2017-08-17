using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cc.common.Utility;
using cc.common;
using cc.common.core;

namespace cc.webapi.Controllers
{
    public class UserAuthController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage IsLogin([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                ActionResult<int> result = new ActionResult<int>();
                result.ResponseID = 0;


                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    result.Data = 0;
                }
                else
                {
                    result.Data = 1;
                }

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage Login([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string userName = jsonPara.GetValueExt<string>("username");
                string password = jsonPara.GetValueExt<string>("password");
                string openid = jsonPara.GetValueExt<string>("openid", "");
                string access_token = jsonPara.GetValueExt<string>("access_token", "");
                #endregion

                cc.iservices.IUserAuth userAuthBC = new cc.services.UserAuth();
                var result = userAuthBC.Login(userName, password, openid, access_token);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetUserInfo([FromBody]JObject jsonPara)
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

                ActionResult<UserInfo> result = cc.common.Utility.MyResponse.ToYou<UserInfo>(loginUser);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };

            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage LogOut([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                cc.common.TokenMng.TokenManage.RemoveUser(token);

                ActionResult<int> result = cc.common.Utility.MyResponse.ToYou<int>(1, "退出登录成功!");

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetUserMenu([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IUserAuth userAuth = new cc.services.UserAuth();

                var result = userAuth.GetUserMenu(loginUser);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }

        }

        [HttpPost]
        public HttpResponseMessage ApplyOpenSupplier([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IUserAuth userAuth = new cc.services.UserAuth();

                var result = userAuth.ApplyOpenSupplier(loginUser);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }

        }

        [HttpPost]
        public HttpResponseMessage Reg([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string userName = jsonPara.GetValueExt<string>("UserName");
                string pwd = jsonPara.GetValueExt<string>("Password");
                string companyName = jsonPara.GetValueExt<string>("CompanyName");
                int regSource = jsonPara.GetValueExt<int>("RegSource", 0);
                string inviteCode = jsonPara.GetValueExt<string>("inviteCode", "");
                #endregion


                cc.iservices.IUserAuth userAuth = new cc.services.UserAuth();

                var result = userAuth.Reg(userName, pwd, regSource, companyName, inviteCode);

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
