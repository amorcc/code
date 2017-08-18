using cc.iservices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.services
{
    public class CompanyMng : ICompanyMng
    {

        common.Utility.ActionResult<model.VCompanyInfo> ICompanyMng.GetCompanyInfo(common.UserInfo iLoginUser)
        {
            cc.unit.CompanyMng.CompanyMng companyBU = new unit.CompanyMng.CompanyMng();
            var result = companyBU.GetCompanyInfo(iLoginUser);

            return cc.common.Utility.MyResponse.ToYou<model.VCompanyInfo>(result);
        }


        common.Utility.ActionResult<int> ICompanyMng.CreateInviteQRCode(common.UserInfo iLoginUser)
        {
            string errorMsg = string.Empty;

            cc.unit.CompanyMng.CompanyMng companyBC = new unit.CompanyMng.CompanyMng();
            var result = companyBC.CreateInviteQRCode(iLoginUser, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<JObject> ICompanyMng.JoinMe(common.UserInfo iLoginUser, string iUserSN_S)
        {
            string errorMsg = string.Empty;

            cc.unit.CompanyMng.CompanyMng companyBC = new unit.CompanyMng.CompanyMng();
            var rt = companyBC.JoinMe(iLoginUser, iUserSN_S, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                JObject result = new JObject()
                {
                    {"IsLogin",1},
                    {"Success",1},
                };
                return cc.common.Utility.MyResponse.ToYou<JObject>(result, "操作成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JObject>(errorMsg);
            }
        }





        common.Utility.ActionResult<int> ICompanyMng.UpdateCompanyInfo(common.UserInfo iLoginUser, int iId, string iCompanyName, int iAreaCode, string iCompanyPhone, string iCompanyAddress, string iBusinessScope, string iShopName, string iWechatNumber, string iLogoImgUrl)
        {
            string errorMsg = string.Empty;

            cc.unit.CompanyMng.CompanyMng companyBC = new unit.CompanyMng.CompanyMng();
            var rt = companyBC.UpdateCompanyInfo(iLoginUser, iId, iCompanyName, iAreaCode, iCompanyPhone, iCompanyAddress, iBusinessScope, iShopName, iWechatNumber, iLogoImgUrl, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {

                return cc.common.Utility.MyResponse.ToYou<int>(rt, "操作成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }
    }
}
