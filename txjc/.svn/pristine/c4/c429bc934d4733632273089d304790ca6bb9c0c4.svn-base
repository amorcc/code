using cc.common.TokenMng;
using cc.iservices;
using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.services
{
    public class UserAuth : IUserAuth
    {
        #region 登录
        common.Utility.ActionResult<common.UserInfo> IUserAuth.Login(string iUserName, string iPassword, string iOpenid, string access_token)
        {
            string token = System.Guid.NewGuid().ToString("N");
            string errorMsg = string.Empty;

            cc.unit.User.UserAuth userAuthBU = new unit.User.UserAuth();
            cc.common.UserInfo user = userAuthBU.Login(iUserName, iPassword, token, iOpenid, access_token, out errorMsg);

            if (user != null)
            {
                #region 写token信息到webcache
                //    //写入到Token的webcache里
                if (!TokenManage.AddUser(token, user))
                {
                    return cc.common.Utility.MyResponse.ShowError<common.UserInfo>("将Token信息写入webcache时出错！");
                }
                #endregion

                #region 往哨兵写登录日志
                //zmm.log.Log.LoginLog(iUserName, userInfo.RoleId, (int)iTargetSite, responseId, iIP, iMisc, iPartnerId);
                #endregion

                return cc.common.Utility.MyResponse.ToYou<common.UserInfo>(user);
            }

            if (string.IsNullOrEmpty(errorMsg) && user != null)
            {
                return cc.common.Utility.MyResponse.ToYou<common.UserInfo>(user);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<common.UserInfo>(errorMsg);
            }
        }

        #endregion

        common.Utility.ActionResult<common.UserInfo> IUserAuth.LoginByOpenid(string iOpenid)
        {
            string token = System.Guid.NewGuid().ToString("N");
            string errorMsg = string.Empty;

            cc.unit.User.UserAuth userAuthBU = new unit.User.UserAuth();
            cc.common.UserInfo user = userAuthBU.LoginByOpenid(iOpenid, token, out errorMsg);

            if (user != null)
            {
                #region 写token信息到webcache
                //    //写入到Token的webcache里
                if (!TokenManage.AddUser(token, user))
                {
                    return cc.common.Utility.MyResponse.ShowError<common.UserInfo>("将Token信息写入webcache时出错！");
                }
                #endregion

                #region 往哨兵写登录日志
                //zmm.log.Log.LoginLog(iUserName, userInfo.RoleId, (int)iTargetSite, responseId, iIP, iMisc, iPartnerId);
                #endregion

                return cc.common.Utility.MyResponse.ToYou<common.UserInfo>(user);
            }

            if (string.IsNullOrEmpty(errorMsg) && user != null)
            {
                return cc.common.Utility.MyResponse.ToYou<common.UserInfo>(user);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<common.UserInfo>(errorMsg);
            }
        }

        common.Utility.ActionResult<List<MenuInfo>> IUserAuth.GetUserMenu(common.UserInfo iLoginUser)
        {
            string errorMsg = string.Empty;


            cc.unit.User.UserAuth userBU = new unit.User.UserAuth();
            List<MenuInfo> result = userBU.GetUserMenu(iLoginUser, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<List<MenuInfo>>(result, "");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<List<MenuInfo>>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IUserAuth.ApplyOpenSupplier(common.UserInfo iLoginUser)
        {
            string errorMsg = string.Empty;


            cc.unit.User.UserAuth userBU = new unit.User.UserAuth();
            var result = userBU.ApplyOpenSupplier(iLoginUser, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IUserAuth.Reg(string iUserName, string iPassword, int iRegSource, string iCompanyName, string iInviteCode)
        {
            string errorMsg = string.Empty;


            cc.unit.User.UserAuth userBU = new unit.User.UserAuth();
            var result = userBU.Reg(iUserName, iPassword, iRegSource, iCompanyName, iInviteCode, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }



    }
}
