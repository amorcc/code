using cc.iservices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.services
{
    public class PayMng : IPayMng
    {
        common.Utility.ActionResult<Newtonsoft.Json.Linq.JArray> IPayMng.GetSupplierPayType(common.UserInfo iLoginUser)
        {
            string errorMsg = string.Empty;

            cc.unit.PayMng.PayMng payBU = new unit.PayMng.PayMng();

            var result = payBU.GetSupplierPayType(iLoginUser, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<JArray>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JArray>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IPayMng.SetSupplierPayType(common.UserInfo iLoginUser, int iPayTypeId, int iIsOpen)
        {
            string errorMsg = string.Empty;

            cc.unit.PayMng.PayMng payBU = new unit.PayMng.PayMng();

            var result = payBU.SetSupplierPayType(iLoginUser, iPayTypeId, iIsOpen, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result, "操作成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }
    }
}
