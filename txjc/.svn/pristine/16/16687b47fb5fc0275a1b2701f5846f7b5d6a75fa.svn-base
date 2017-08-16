using cc.common;
using cc.common.core;
using cc.common.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cc.webapi.Controllers
{
    public class ProductOrderController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Preview([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string batchId = jsonPara.GetValueExt<string>("batchid");
                int orderSource = jsonPara.GetValueExt<int>("ordersource", 1);
                string proInfo = jsonPara.GetValueExt<string>("proinfo", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.GetPreviewInfo(loginUser, proInfo, batchId, orderSource);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage OrderCreate([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string proInfo = jsonPara.GetValueExt<string>("ProInfo", "");
                int orderSource = jsonPara.GetValueExt<int>("OrderSource", 0);
                string rn = jsonPara.GetValueExt<string>("RN", "");
                JArray orderInfo = jsonPara.GetValueExt<JArray>("OrderInfo");
                int addressId = jsonPara.GetValueExt<int>("AddressId", 0);
                int partnerId = jsonPara.GetValueExt<int>("PartnerId", 200);
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.OrderCreate(loginUser, proInfo, rn, orderSource, addressId, orderInfo, partnerId);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetPayPreviewInfo([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string batchId = jsonPara.GetValueExt<string>("BatchId", "");
                string orderCodes = jsonPara.GetValueExt<string>("OrderCodes", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.GetPayPreviewInfo(loginUser, batchId, orderCodes);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage OrderPay([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int payType = jsonPara.GetValueExt<int>("PayType", 1);
                string orderCodes = jsonPara.GetValueExt<string>("OrderCodes", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.OrderPay(loginUser, orderCodes, payType);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetBuyersList([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int pageIndex = jsonPara.GetValueExt<int>("PageIndex", 1);
                int pageSize = jsonPara.GetValueExt<int>("PageSize", 15);
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                int orderStatus = jsonPara.GetValueExt<int>("OrderStatus", -2);
                string supplierName = jsonPara.GetValueExt<string>("SupplierName", "");
                string startDate = jsonPara.GetValueExt<string>("StartDate", "");
                string endDate = jsonPara.GetValueExt<string>("EndDate", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.GetBuyersList(loginUser, orderCode, orderStatus, supplierName, startDate, endDate, pageIndex, pageSize);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetSellerList([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                int pageIndex = jsonPara.GetValueExt<int>("PageIndex", 1);
                int pageSize = jsonPara.GetValueExt<int>("PageSize", 15);
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                int orderStatus = jsonPara.GetValueExt<int>("OrderStatus", -2);
                string retailerName = jsonPara.GetValueExt<string>("RetailerName", "");
                string startDate = jsonPara.GetValueExt<string>("StartDate", "");
                string endDate = jsonPara.GetValueExt<string>("EndDate", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.GetSellerList(loginUser, orderCode, orderStatus, retailerName, startDate, endDate, pageIndex, pageSize);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage BuyersOrderCancel([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.BuyersOrderCancel(loginUser, orderCode);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage SellerOrderCancel([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.SellerOrderCancel(loginUser, orderCode);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage SellerStoreOutBefore([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.SellerStoreOutBefore(loginUser, orderCode);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage SellerStoreOut([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                string storeOutInfo = jsonPara.GetValueExt<string>("StoreOutInfo", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.SellerStoreOut(loginUser, orderCode, storeOutInfo);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetDeliverGoodsInfo([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.GetDeliverGoodsInfo(loginUser, orderCode);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage SellerDeliverGoodsInfo([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string storeOutCode = jsonPara.GetValueExt<string>("StoreOutCode", "");
                int expId = jsonPara.GetValueExt<int>("ExpId", 0);
                string expNum = jsonPara.GetValueExt<string>("ExpNum", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.SellerDeliverGoodsInfo(loginUser, storeOutCode, expId, expNum);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage ConfirmReceipt([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.ConfirmReceipt(loginUser, orderCode);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage OrderWriteOff([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.OrderWriteOff(loginUser, orderCode);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetOrderInfo([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.GetOrderInfo(loginUser, orderCode);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage OrderChangePrice([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCode = jsonPara.GetValueExt<string>("OrderCode", "");
                string newTransFee = jsonPara.GetValueExt<string>("NewTransFee", "");
                string newProPrice = jsonPara.GetValueExt<string>("NewProPrice", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IProductOrder poBC = new cc.services.ProductOrder();
                var result = poBC.OrderChangePrice(loginUser, orderCode, newTransFee, newProPrice);

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
