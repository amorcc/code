using cc.common;
using cc.iservices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.services
{
    public class CartMng : ICartMng
    {
        common.Utility.ActionResult<int> ICartMng.GetCartCount(common.UserInfo iLoginUser)
        {
            cc.unit.CartMng.CartMng cartBU = new unit.CartMng.CartMng();
            int result = cartBU.GetCartCount(iLoginUser.UserSN);

            return cc.common.Utility.MyResponse.ToYou<int>(result);
        }


        common.Utility.ActionResult<Newtonsoft.Json.Linq.JArray> ICartMng.GetCartInfo(common.UserInfo iLoginUser)
        {
            cc.unit.CartMng.CartMng cartBU = new unit.CartMng.CartMng();
            var result = cartBU.GetCartInfo(iLoginUser.UserSN);

            return cc.common.Utility.MyResponse.ToYou<JArray>(result);
        }


        common.Utility.ActionResult<int> ICartMng.CartModifyCount(UserInfo iLoginUser, int iProId, int iModifyCount)
        {
            string errorMsg = string.Empty;


            cc.unit.CartMng.CartMng cartBU = new unit.CartMng.CartMng();
            var result = cartBU.CartModifyCount(iLoginUser, iProId, iModifyCount, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result ? 1 : 0, "操作成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }
    }
}
