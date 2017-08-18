using cc.common.Utility;
using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.iservices
{
    public interface ICompanyMng
    {
        ActionResult<VCompanyInfo> GetCompanyInfo(common.UserInfo iLoginUser);
        ActionResult<VCompanyInfo> GetCompanyInfo(string iUserSN_S);

        ActionResult<int> CreateInviteQRCode(common.UserInfo iLoginUser);

        ActionResult<JObject> JoinMe(common.UserInfo iLoginUser, string iUserSN_S);

        ActionResult<int> UpdateCompanyInfo(common.UserInfo iLoginUser, int iId, string iCompanyName, int iAreaCode, string iCompanyPhone, string iCompanyAddress, string iBusinessScope, string iShopName, string iWechatNumber, string iLogoImgUrl);
    }
}
