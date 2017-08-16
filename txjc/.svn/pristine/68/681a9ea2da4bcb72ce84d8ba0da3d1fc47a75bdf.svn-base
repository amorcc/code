using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.common.Utility;
using Newtonsoft.Json.Linq;
using cc.common;
using cc.model;

namespace cc.iservices
{
    public interface IUserAuth
    {
        ActionResult<cc.common.UserInfo> Login(string iUserName, string iPassword, string iOpenid, string access_token);
        ActionResult<cc.common.UserInfo> LoginByOpenid(string iOpenid);
        ActionResult<List<MenuInfo>> GetUserMenu(cc.common.UserInfo iLoginUser);

        ActionResult<int> ApplyOpenSupplier(common.UserInfo iLoginUser);

        ActionResult<int> Reg(string iUserName, string iPassword, int iRegSource, string iCompanyName, string iInviteCode);
    }
}
