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
    public interface ICartMng
    {
        ActionResult<int> GetCartCount(cc.common.UserInfo iLoginUser);

        ActionResult<JArray> GetCartInfo(cc.common.UserInfo iLoginUser);

        ActionResult<int> CartModifyCount(UserInfo iLoginUser, int iProId, int iModifyCount);
    }
}
