using cc.iservices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.services
{
    public class Alipay : IAlipay
    {
        common.Utility.ActionResult<Newtonsoft.Json.Linq.JObject> IAlipay.Alipay(common.UserInfo iLoginUser, string iRN, string iOrderCodes, int iPayType)
        {
            string errorMsg = string.Empty;


            cc.unit.Alipay.AlipayMng payBU = new unit.Alipay.AlipayMng();
            var result = payBU.Alipay(iLoginUser, iRN, iOrderCodes, iPayType, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<JObject>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JObject>(errorMsg);
            }
        }
    }
}
