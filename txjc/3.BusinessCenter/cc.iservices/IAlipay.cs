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
    public interface IAlipay
    {
        ActionResult<JObject> Alipay(common.UserInfo iLoginUser, string iRN, string iOrderCodes, int iPayType);
    }
}
