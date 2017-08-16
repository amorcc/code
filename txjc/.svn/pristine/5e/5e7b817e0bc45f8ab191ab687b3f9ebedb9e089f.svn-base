using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.common.Utility;
using cc.model;
using Newtonsoft.Json.Linq;


namespace cc.iservices
{
    public interface IRetailer
    {
        ActionResult<JObject> GetMySupplier(common.UserInfo iLoginUser, int iPageIndex, int iPageSize);

        ActionResult<VCompanyInfo> SearchSupplier(common.UserInfo iLoginUser, string iUserSN);

        ActionResult<int> AddSupplier(common.UserInfo iLoginUser, string iUserSN_S, string iUserSN_R);

        ActionResult<int> RemoveSupplier(common.UserInfo iLoginUser, int iUserCanBuyId);
    }
}
