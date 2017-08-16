using cc.common;
using cc.common.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.iservices
{
    public interface IProductMng
    {
        ActionResult<int> AddToCart(int iUserId, string iUserSN_R, int iProId, int iProCount);
        ActionResult<JObject> GetRetailerCanBuyProductList(UserInfo iLoginUser, string iKey, int iPageIndex, int iPageSize);
        ActionResult<JObject> GetRetailerCanBuyProductList2(UserInfo iLoginUser, int iPageIndex, int iPageSize);

        ActionResult<JObject> GetProductInfo(UserInfo iLoginUser, int iProId);

        ActionResult<JObject> SellerProductMng(common.UserInfo iLoginUser, int iStatus, string iProName, int iPageIndex, int iPageSize);

        ActionResult<int> UpdateStatus(common.UserInfo iLoginUser, int iProId, int iStatus);

        ActionResult<int> ModifyProduct(common.UserInfo iLoginUser, int iProId, string iProName, int iBillNeeded, int iStatus, int iAmount, decimal iPrice, string iImages, string iDesc);

        ActionResult<int> DeleteProduct(common.UserInfo iLoginUser, int iProId);

        ActionResult<int> UpdateAmount(common.UserInfo iLoginUser, int iProId, int iAmount);

        ActionResult<int> UpdatePrice(common.UserInfo iLoginUser, int iProId, decimal iPrice);

        ActionResult<JObject> GetMyCollectProductInfo(cc.common.UserInfo iLoginUser, int iPageIndex, int iPageSize);

        ActionResult<int> IsCollect(common.UserInfo iLoginUser, int iProId);

        ActionResult<int> ProductCollectSwitch(common.UserInfo iLoginUser, int iProId);
    }
}
