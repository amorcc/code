using cc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.User
{
    /// <summary>
    /// 用户认证相关
    /// </summary>
    public class UserAuth
    {
        public cc.common.UserInfo Login(string iUserName, string iPassword, string iToken, string iOpenid, string access_token, out string iErrorMsg)
        {
            cc.dal.User userDal = new dal.User();

            return userDal.Login(iUserName, iPassword, iToken, iOpenid, access_token, out iErrorMsg);
        }

        #region openid登录
        public common.UserInfo LoginByOpenid(string iOpenid, string iToken, out string iErrorMsg)
        {
            cc.dal.User userDal = new dal.User();
            return userDal.LoginByOpenid(iOpenid, iToken, out iErrorMsg);
        }
        #endregion

        #region 返回用户菜单
        public List<MenuInfo> GetUserMenu(cc.common.UserInfo iLoginUser, out string iErrorMsg)
        {
            cc.dal.User userDal = new dal.User();
            return userDal.GetUserMenu(iLoginUser, out iErrorMsg);
        }
        #endregion

        #region 开通卖家
        public bool ApplyOpenSupplier(common.UserInfo iLoginUser, out string iErrorMsg)
        {
            cc.dal.User userDal = new dal.User();
            return userDal.ApplyOpenSupplier(iLoginUser, out iErrorMsg);
        }
        #endregion

        #region 注册
        public bool Reg(string iUserName, string iPassword, int iRegSource, string iCompanyName, string iInviteCode, out string iErrorMsg)
        {
            cc.dal.User userDal = new dal.User();
            return userDal.Reg(iUserName, iPassword, iRegSource, iCompanyName, iInviteCode, out iErrorMsg);
        }
        #endregion
    }
}
