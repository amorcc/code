using cc.common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cc.common.Utility;
using cc.common.core;

namespace cc.webapi.Controllers
{
    public class CartController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetCartNum([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    ActionResult<int> result1 = new ActionResult<int>();
                    result1.ResponseID = 0;
                    result1.Data = 0;


                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result1), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.ICartMng cartBC = new cc.services.CartMng();

                var result = cartBC.GetCartCount(loginUser);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetCartInfo([FromBody]JObject jsonPara)
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

                cc.iservices.ICartMng cartBC = new cc.services.CartMng();

                var result = cartBC.GetCartInfo(loginUser);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage AddToCartManyPro([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token");
                JArray proList = jsonPara.GetValueExt<JArray>("ProList");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                if (proList != null && proList.Count > 0)
                {
                    foreach (JObject item in proList)
                    {
                        int proId = item.GetValueExt<int>("ProId");
                        int proCount = item.GetValueExt<int>("ProCount");
                        productBC.AddToCart(loginUser.UserId, loginUser.UserSN, proId, proCount);

                    }
                }

                var result = cc.common.Utility.MyResponse.ToYou<int>(1, "已加入到购物车");

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage CartModifyCount([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int proId = jsonPara.GetValueExt<int>("ProId", 0);
                int modifyCount = jsonPara.GetValueExt<int>("ModifyCount", 0);

                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.ICartMng cartBC = new cc.services.CartMng();

                var result = cartBC.CartModifyCount(loginUser, proId, modifyCount);

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
