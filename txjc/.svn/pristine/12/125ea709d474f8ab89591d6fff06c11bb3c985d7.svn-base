using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cc.common.Utility;
using Newtonsoft.Json;
using cc.common;
using cc.common.core;

namespace cc.webapi.Controllers
{
    public class SupplierController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetMyRetailer([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int pageIndex = jsonPara.GetValueExt<int>("PageIndex", 1);
                int pageSize = jsonPara.GetValueExt<int>("PageSize", 15);
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.ISupplier sBC = new cc.services.Supplier();
                var result = sBC.GetMyRetailer(loginUser, pageIndex, pageSize);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }
        public HttpResponseMessage AddRetailer([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string userSN_R = jsonPara.GetValueExt<string>("UserSN_R", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IRetailer rBC = new cc.services.Retailer();
                var result = rBC.AddSupplier(loginUser, loginUser.UserSN, userSN_R);

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
