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
    public interface IWeChat
    {
        ActionResult<JObject> WeChatPay(common.UserInfo iLoginUser, string iOpenId, string iRN, string iOrderCodes, int iPayType);
        ActionResult<JObject> H5Pay(common.UserInfo iLoginUser, string iOpenId, string iRN, string iOrderCodes, int iPayType, string ip);
        ActionResult<string> GetWxJsApiParam(string iOpenId, int iTotalFee);
    }
}
