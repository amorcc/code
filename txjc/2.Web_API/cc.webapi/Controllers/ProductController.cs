using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cc.common.Utility;
using cc.common;
using Newtonsoft.Json;
using cc.common.core;

namespace cc.webapi.Controllers
{
    public class ProductController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AddToCart([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token");
                int proId = jsonPara.GetValueExt<int>("ProId", 0);
                int proCount = jsonPara.GetValueExt<int>("ProCount", 1);
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.AddToCart(loginUser.UserId, loginUser.UserSN, proId, proCount);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }



        }

        [HttpPost]
        public HttpResponseMessage GetRetailerCanBuyProductList([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token");
                string key = jsonPara.GetValueExt<string>("key", "");
                int pageIndex = jsonPara.GetValueExt<int>("PageIndex", 1);
                int pageSize = jsonPara.GetValueExt<int>("PageSize", 15);
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.GetRetailerCanBuyProductList(loginUser, key, pageIndex, pageSize);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }



        }

        [HttpPost]
        public HttpResponseMessage GetProductInfo([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token");
                int proId = jsonPara.GetValueExt<int>("ProId", 0);
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.GetProductInfo(loginUser, proId);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }



        }


        [HttpPost]
        public HttpResponseMessage SellerProductMng([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int status = jsonPara.GetValueExt<int>("Status", 1);
                string proName = jsonPara.GetValueExt<string>("Name", "");
                int pageIndex = jsonPara.GetValueExt<int>("PageIndex", 1);
                int pageSize = jsonPara.GetValueExt<int>("PageSize", 15);
                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.SellerProductMng(loginUser, status, proName, pageIndex, pageSize);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }



        }

        [HttpPost]
        public HttpResponseMessage UpdateStatus([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int status = jsonPara.GetValueExt<int>("Status", 0);
                int proId = jsonPara.GetValueExt<int>("ProId", 0);
                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.UpdateStatus(loginUser, proId, status);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }



        }

        [HttpPost]
        public HttpResponseMessage ModifyProduct([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int proId = jsonPara.GetValueExt<int>("ProId", 0);
                int status = jsonPara.GetValueExt<int>("Status", 0);
                string proName = jsonPara.GetValueExt<string>("ProName", "");
                int billNeeded = jsonPara.GetValueExt<int>("BillNeeded", 0);
                int amount = jsonPara.GetValueExt<int>("Amount", 0);
                decimal price = jsonPara.GetValueExt<decimal>("Price", 0);
                string imagesStr = jsonPara.GetValueExt<string>("Images", "");
                string desc = jsonPara.GetValueExt<string>("Desc", "");

                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.ModifyProduct(loginUser, proId, proName, billNeeded, status, amount, price, imagesStr, desc);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }



        }

        [HttpPost]
        public HttpResponseMessage DeleteProduct([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int proId = jsonPara.GetValueExt<int>("ProId", 0);

                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.DeleteProduct(loginUser, proId);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }



        }

        [HttpPost]
        public HttpResponseMessage UpdateAmount([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int proId = jsonPara.GetValueExt<int>("ProId", 0);
                int amount = jsonPara.GetValueExt<int>("Amount", -1);

                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.UpdateAmount(loginUser, proId, amount);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdatePrice([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int proId = jsonPara.GetValueExt<int>("ProId", 0);
                decimal price = jsonPara.GetValueExt<decimal>("Price", 0);

                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.UpdatePrice(loginUser, proId, price);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetMyCollectProductInfo([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int pageIndex = jsonPara.GetValueExt<int>("PageIndex", 1);
                int pageSize = jsonPara.GetValueExt<int>("PageSize", 15);
                string token = jsonPara.GetValueExt<string>("token");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.GetMyCollectProductInfo(loginUser, pageIndex, pageSize);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage IsCollect([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token");
                int proId = jsonPara.GetValueExt<int>("ProId", 0);
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.IsCollect(loginUser, proId);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage ProductCollectSwitch([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string token = jsonPara.GetValueExt<string>("token");
                int proId = jsonPara.GetValueExt<int>("ProId", 0);
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductMng productBC = new cc.services.ProductMng();

                var result = productBC.ProductCollectSwitch(loginUser, proId);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(ProductController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }
    }
}
