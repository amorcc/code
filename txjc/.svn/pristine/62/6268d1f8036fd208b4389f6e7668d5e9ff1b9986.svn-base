using cc.iservices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.services
{
    public class WeChat : IWeChat
    {

        common.Utility.ActionResult<string> IWeChat.GetWxJsApiParam(string iOpenId, int iTotalFee)
        {
            string errorMsg = string.Empty;

            cc.unit.WeChat.Pay.PayMng payBU = new unit.WeChat.Pay.PayMng();
            string result = payBU.GetWxJsApiParam(iOpenId, iTotalFee, System.Guid.NewGuid().ToString("N"), "", out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<string>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<string>(errorMsg);
            }
        }

        common.Utility.ActionResult<Newtonsoft.Json.Linq.JObject> IWeChat.WeChatPay(common.UserInfo iLoginUser, string iOpenId, string iRN, string iOrderCodes, int iPayType)
        {
            string errorMsg = string.Empty;

            cc.unit.WeChat.Pay.PayMng payBU = new unit.WeChat.Pay.PayMng();
            var result = payBU.WeChatPay(iLoginUser, iRN, iOpenId, iOrderCodes, iPayType, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<JObject>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JObject>(errorMsg);
            }
        }


        common.Utility.ActionResult<JObject> IWeChat.H5Pay(common.UserInfo iLoginUser, string iOpenId, string iRN, string iOrderCodes, int iPayType, string iIp)
        {
            string errorMsg = string.Empty;

            cc.unit.WeChat.Pay.PayMng payBU = new unit.WeChat.Pay.PayMng();
            var result = payBU.H5Pay(iLoginUser, iRN, iOpenId, iOrderCodes, iPayType, iIp, out errorMsg);

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
