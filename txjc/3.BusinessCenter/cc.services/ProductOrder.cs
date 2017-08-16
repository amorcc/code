using cc.iservices;
using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.services
{
    public class ProductOrder : IProductOrder
    {
        common.Utility.ActionResult<Newtonsoft.Json.Linq.JObject> IProductOrder.GetPreviewInfo(common.UserInfo iLoginUser, string iProInfoStr, string iBatchId, int iOrderSource)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.Preview.Preview perview = new unit.ProductOrder.Preview.Preview(iLoginUser, iProInfoStr, (unit.ProductOrder.Preview.OrderSource)iOrderSource, iBatchId);
            var result = perview.GetPreviewInfo(out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<JObject>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JObject>(errorMsg);
            }
        }


        common.Utility.ActionResult<JObject> IProductOrder.OrderCreate(common.UserInfo iLoginUser, string iProInfoStr, string iBatchId, int iOrderSource, int iAddressId, JArray iOrderCreateInfo, int iPartnerId)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductOrder.Create.OrderCreate oc = new unit.ProductOrder.Create.OrderCreate(iLoginUser, iProInfoStr, iBatchId, (unit.ProductOrder.Preview.OrderSource)iOrderSource, iAddressId, iOrderCreateInfo, iPartnerId);

            var result = oc.Create(out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<JObject>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JObject>(errorMsg);
            }
        }


        common.Utility.ActionResult<JObject> IProductOrder.GetPayPreviewInfo(common.UserInfo iLoginUser, string iBatchId, string iOrderCodes)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductOrder.Pay.PayPreview ppBU = new unit.ProductOrder.Pay.PayPreview();
            var result = ppBU.GetPayPreviewInfo(iLoginUser, iBatchId, iOrderCodes, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<JObject>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JObject>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IProductOrder.OrderPay(common.UserInfo iLoginUser, string iOrderCodes, int iPayType)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductOrder.Pay.Pay payBU = new unit.ProductOrder.Pay.Pay();
            var result = payBU.OrderPay(iLoginUser, iOrderCodes, iPayType, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<JObject> IProductOrder.GetBuyersList(cc.common.UserInfo iLoginUser, string iOrderCode, int iOrderStatus, string iSupplierName, string iStartDate, string iEndDate, int iPageIndex, int iPageSize)
        {
            string errorMsg = string.Empty;
            int totalRows = 0;
            int totalPages = 1;

            cc.unit.ProductOrder.ProductOrder poBU = new unit.ProductOrder.ProductOrder();
            List<VProductOrderInfo> list = poBU.GetBuyersList(iLoginUser, iOrderCode, iOrderStatus, iSupplierName, iStartDate, iEndDate, iPageIndex, iPageSize, out totalRows, out totalPages);

            JArray data = new JArray();

            if (list != null)
            {
                foreach (var item in list)
                {
                    int sumProCount = 0;
                    decimal sumTotal = 0;
                    decimal sumTransFee = 0;

                    JArray detailJA = new JArray();
                    if (item.DetailList != null)
                    {
                        foreach (var item0 in item.DetailList)
                        {
                            sumProCount += item0.ProCount;
                            sumTotal += item0.SubTotal + item0.TransFee;
                            sumTransFee += item0.TransFee;

                            detailJA.Add(new JObject()
                            {
                                {"OrderCode",item0.OrderCode},
                                {"ProCount",item0.ProCount},
                                {"ProId",item0.ProId},
                                {"ProImage",cc.utility.Common.App("ApiSiteUrl")+ item0.ProImage},
                                {"ProName",item0.ProName},
                                {"ProPrice",item0.ProPrice.ToString("0.00")},
                                {"ReturnCount",item0.ReturnCount},
                                {"SendCount",item0.SendCount},
                                {"StoreOutNum",item0.StoreOutNum},
                                {"SubOrderCode",item0.SubOrderCode},
                                {"SubTotal",item0.SubTotal.ToString("0.00")},
                                {"TransFee",item0.TransFee.ToString("0.00")},
                            });
                        }

                    }

                    data.Add(new JObject()
                    {
                        {"OrderCode",item.OrderCode},
                        {"DateAdded",item.DateAdded},
                        {"OrderStatus",item.OrderStatus},
                        {"Supplier",item.Supplier},
                        {"Message",item.Message},
                        {"BatchId",item.BatchId},
                        {"Total",sumTotal.ToString("0.00")},
                        {"SumProCount",sumProCount},
                        {"SumTransFee",sumTransFee.ToString("0.00")},
                        {"Details",detailJA},
                    });
                }
            }

            JObject result = new JObject()
            {
                {"Data",data},
                {"TotalPages",totalPages},
                {"TotalRows",totalRows},
            };

            return cc.common.Utility.MyResponse.ToYou<JObject>(result);
        }


        common.Utility.ActionResult<int> IProductOrder.BuyersOrderCancel(common.UserInfo iLoginUser, string iOrderCode)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.ProductOrder poBU = new unit.ProductOrder.ProductOrder();
            var result = poBU.BuyersOrderCancel(iLoginUser, iOrderCode, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result ? 1 : 0, "取消订单成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }

        common.Utility.ActionResult<int> IProductOrder.SellerOrderCancel(common.UserInfo iLoginUser, string iOrderCode)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.ProductOrder poBU = new unit.ProductOrder.ProductOrder();
            var result = poBU.SellerOrderCancel(iLoginUser, iOrderCode, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result ? 1 : 0, "取消订单成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<JObject> IProductOrder.GetSellerList(common.UserInfo iLoginUser, string iOrderCode, int iOrderStatus, string iRetailerName, string iStartDate, string iEndDate, int iPageIndex, int iPageSize)
        {
            string errorMsg = string.Empty;
            int totalRows = 0;
            int totalPages = 1;

            cc.unit.ProductOrder.ProductOrder poBU = new unit.ProductOrder.ProductOrder();
            List<VProductOrderInfo> list = poBU.GetSellerList(iLoginUser, iOrderCode, iOrderStatus, iRetailerName, iStartDate, iEndDate, iPageIndex, iPageSize, out totalRows, out totalPages);

            JArray data = new JArray();

            if (list != null)
            {
                foreach (var item in list)
                {
                    int sumProCount = 0;
                    decimal sumTotal = 0;
                    decimal sumTransFee = 0;

                    JArray detailJA = new JArray();
                    if (item.DetailList != null)
                    {
                        foreach (var item0 in item.DetailList)
                        {
                            sumProCount += item0.ProCount;
                            sumTotal += item0.SubTotal + item0.TransFee;
                            sumTransFee += item0.TransFee;

                            detailJA.Add(new JObject()
                            {
                                {"OrderCode",item0.OrderCode},
                                {"ProCount",item0.ProCount},
                                {"ProId",item0.ProId},
                                {"ProImage",cc.utility.Common.App("ApiSiteUrl")+item0.ProImage},
                                {"ProName",item0.ProName},
                                {"ProPrice",item0.ProPrice.ToString("0.00")},
                                {"ReturnCount",item0.ReturnCount},
                                {"SendCount",item0.SendCount},
                                {"StoreOutNum",item0.StoreOutNum},
                                {"SubOrderCode",item0.SubOrderCode},
                                {"SubTotal",item0.SubTotal.ToString("0.00")},
                                {"TransFee",item0.TransFee.ToString("0.00")},
                            });
                        }

                    }

                    data.Add(new JObject()
                    {
                        {"OrderCode",item.OrderCode},
                        {"DateAdded",item.DateAdded},
                        {"OrderStatus",item.OrderStatus},
                        {"Supplier",item.Supplier},
                        {"Message",item.Message},
                        {"BatchId",item.BatchId},
                        {"Total",sumTotal.ToString("0.00")},
                        {"SumProCount",sumProCount},
                        {"SumTransFee",sumTransFee.ToString("0.00")},
                        {"Details",detailJA},
                    });
                }
            }

            JObject result = new JObject()
            {
                {"Data",data},
                {"TotalPages",totalPages},
                {"TotalRows",totalRows},
            };

            return cc.common.Utility.MyResponse.ToYou<JObject>(result);
        }


        common.Utility.ActionResult<JObject> IProductOrder.SellerStoreOutBefore(common.UserInfo iLoginUser, string iOrderCode)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.ProductOrder poBU = new unit.ProductOrder.ProductOrder();
            VProductOrderInfo poInfo = poBU.SellerStoreOutBefore(iLoginUser, iOrderCode, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg) && poInfo != null && poInfo.DetailList != null)
            {
                JArray detailJA = new JArray();
                foreach (var item0 in poInfo.DetailList)
                {
                    detailJA.Add(new JObject()
                    {
                        {"OrderCode",item0.OrderCode},
                        {"ProCount",item0.ProCount},
                        {"ProId",item0.ProId},
                        {"ProImage",item0.ProImage},
                        {"ProName",item0.ProName},
                        {"ProPrice",item0.ProPrice.ToString("0.00")},
                        {"ReturnCount",item0.ReturnCount},
                        {"SendCount",item0.SendCount},
                        {"StoreOutNum",item0.StoreOutNum},
                    });
                }

                JObject result = new JObject()
                {
                    {"OrderCode",poInfo.OrderCode},
                    {"DateAdded",poInfo.DateAdded},
                    {"OrderStatus",poInfo.OrderStatus},
                    {"Supplier",poInfo.Supplier},
                    {"Retailer",poInfo.Retailer},
                    {"Message",poInfo.Message},
                    {"BatchId",poInfo.BatchId},
                    {"Details",detailJA},
                };


                return cc.common.Utility.MyResponse.ToYou<JObject>(result);
            }


            errorMsg = string.IsNullOrEmpty(errorMsg) ? "查询订单信息出错" : errorMsg;
            return cc.common.Utility.MyResponse.ShowError<JObject>(errorMsg);
        }


        common.Utility.ActionResult<int> IProductOrder.SellerStoreOut(common.UserInfo iLoginUser, string iOrderCode, string iStoreOutInfo)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.StoreOut.StoreOut soBU = new unit.ProductOrder.StoreOut.StoreOut(iLoginUser, iOrderCode, iStoreOutInfo);

            bool result = soBU.SellerSotreOut(out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result ? 1 : 0, "出库成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<JObject> IProductOrder.GetDeliverGoodsInfo(common.UserInfo iLoginUser, string iOrderCode)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.DeliverGoods.DeliverGoods dgBU = new unit.ProductOrder.DeliverGoods.DeliverGoods(iLoginUser, iOrderCode);
            var result = dgBU.GetDeliverGoodsInfo(out errorMsg);

            if (string.IsNullOrEmpty(errorMsg) && result != null)
            {
                return cc.common.Utility.MyResponse.ToYou<JObject>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JObject>(string.IsNullOrEmpty(errorMsg) ? "获取出库信息失败" : errorMsg);
            }
        }


        common.Utility.ActionResult<int> IProductOrder.SellerDeliverGoodsInfo(common.UserInfo iLoginUser, string iStoreOutCode, int iExpId, string iExpNum)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.DeliverGoods.DeliverGoods dgBU = new unit.ProductOrder.DeliverGoods.DeliverGoods(iLoginUser, "");

            bool result = dgBU.SellerDeliverGoodsInfo(iLoginUser, iStoreOutCode, iExpId, iExpNum, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result ? 1 : 0, "发货成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IProductOrder.ConfirmReceipt(common.UserInfo iLoginUser, string iOrderCode)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.ProductOrder poBU = new unit.ProductOrder.ProductOrder();

            bool result = poBU.ConfirmReceipt(iLoginUser, iOrderCode, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result ? 1 : 0, "确认收货成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IProductOrder.OrderWriteOff(common.UserInfo iLoginUser, string iOrderCode)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.ProductOrder poBU = new unit.ProductOrder.ProductOrder();

            bool result = poBU.OrderWriteOff(iLoginUser, iOrderCode, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result ? 1 : 0, "操作成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<VProductOrderInfo> IProductOrder.GetOrderInfo(common.UserInfo iLoginUser, string iOrderCode)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.ProductOrder poBU = new unit.ProductOrder.ProductOrder();

            var result = poBU.GetOrderInfo(iLoginUser, iOrderCode, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<VProductOrderInfo>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<VProductOrderInfo>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IProductOrder.OrderChangePrice(common.UserInfo iLoginUser, string iOrderCode, string iNewTransFee, string iNewProPriceInfo)
        {
            string errorMsg = string.Empty;
            cc.unit.ProductOrder.ProductOrder poBU = new unit.ProductOrder.ProductOrder();

            var result = poBU.OrderChangePrice(iLoginUser, iOrderCode, iNewTransFee, iNewProPriceInfo, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result, "设置成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }
    }
}
