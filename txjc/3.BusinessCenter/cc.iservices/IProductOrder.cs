using cc.common.Utility;
using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.iservices
{
    public interface IProductOrder
    {
        ActionResult<JObject> GetPreviewInfo(cc.common.UserInfo iLoginUser, string iProInfoStr, string iBatchId, int iOrderSource);
        ActionResult<JObject> OrderCreate(cc.common.UserInfo iLoginUser, string iProInfoStr, string iBatchId, int iOrderSource, int iAddressId, JArray iOrderCreateInfo, int iPartnerId = 200);

        ActionResult<JObject> GetPayPreviewInfo(cc.common.UserInfo iLoginUser, string iBatchId, string iOrderCodes);
        ActionResult<int> OrderPay(cc.common.UserInfo iLoginUser, string iOrderCodes, int iPayType);

        ActionResult<JObject> GetBuyersList(cc.common.UserInfo iLoginUser, string iOrderCode, int iOrderStatus, string iSupplierName, string iStartDate, string iEndDate, int iPageIndex, int iPageSize);
        ActionResult<JObject> GetSellerList(cc.common.UserInfo iLoginUser, string iOrderCode, int iOrderStatus, string iRetailerName, string iStartDate, string iEndDate, int iPageIndex, int iPageSize);

        ActionResult<int> BuyersOrderCancel(cc.common.UserInfo iLoginUser, string iOrderCode);
        ActionResult<int> SellerOrderCancel(cc.common.UserInfo iLoginUser, string iOrderCode);

        ActionResult<JObject> SellerStoreOutBefore(cc.common.UserInfo iLoginUser, string iOrderCode);

        ActionResult<int> SellerStoreOut(common.UserInfo iLoginUser, string iOrderCode, string iStoreOutInfo);

        ActionResult<JObject> GetDeliverGoodsInfo(common.UserInfo iLoginUser, string iOrderCode);

        ActionResult<int> SellerDeliverGoodsInfo(common.UserInfo iLoginUser, string iStoreOutCode, int iExpId, string iExpNum);

        ActionResult<int> ConfirmReceipt(common.UserInfo iLoginUser, string iOrderCode);

        ActionResult<int> OrderWriteOff(common.UserInfo iLoginUser, string iOrderCode);

        ActionResult<VProductOrderInfo> GetOrderInfo(common.UserInfo iLoginUser, string iOrderCode);

        ActionResult<int> OrderChangePrice(common.UserInfo iLoginUser, string iOrderCode, string iNewTransFee, string iNewProPriceInfo);
    }
}
