using cc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.CompanyMng
{
    public class CompanyMng
    {
        public VCompanyInfo GetCompanyInfo(common.UserInfo iLoginUser)
        {
            cc.dal.Company companyDal = new dal.Company();
            return companyDal.GetCompanyInfo(iLoginUser.UserSN);
        }

        /// <summary>
        /// 生成卖家的邀请二维码
        /// </summary>
        /// <param name="iLoginUser"></param>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public bool CreateInviteQRCode(common.UserInfo iLoginUser, out string iErrorMsg)
        {
            cc.dal.Company companyDal = new dal.Company();
            return companyDal.CreateInviteQRCode(iLoginUser, out iErrorMsg);
        }

        public bool JoinMe(common.UserInfo iLoginUser, string iUserSN_S, out string iErrorMsg)
        {
            cc.dal.Company companyDal = new dal.Company();
            return companyDal.JoinMe(iLoginUser, iUserSN_S, out iErrorMsg);
        }


        public bool UpdateCompanyInfo(common.UserInfo iLoginUser, int iId, string iCompanyName, int iAreaCode, string iCompanyPhone, string iCompanyAddress, string iBusinessScope, string iShopName, string iWechatNumber, string iLogoImgUrl, out string iErrorMsg)
        {

            cc.dal.Company companyDal = new dal.Company();
            return companyDal.UpdateCompanyInfo(iLoginUser, iId, iCompanyName, iAreaCode, iCompanyPhone, iCompanyAddress, iBusinessScope, iShopName, iWechatNumber, iLogoImgUrl, out iErrorMsg);
        }
    }
}
