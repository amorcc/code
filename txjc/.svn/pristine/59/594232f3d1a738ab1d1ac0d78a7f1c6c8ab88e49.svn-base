using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace cc.common.TokenMng
{
    /// <summary>
    /// 管理已经登录用户的token信息
    /// cc  2016-11-19
    /// </summary>
    public class TokenManage
    {
        /// <summary>
        /// 新增一个用户到webcache里保存
        /// </summary>
        /// <param name="iToken"></param>
        /// <param name="iUserInfo"></param>
        /// <returns></returns>
        public static bool AddUser(string iToken, UserInfo iUserInfo)
        {

            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            var tokenCache = objCache["Token"];

            if (tokenCache == null)
            {
                List<UserInfo> users = new List<UserInfo>();
                users.Add(iUserInfo);
                HttpRuntime.Cache.Insert("Token", users);
            }
            else
            {
                List<UserInfo> users = (List<UserInfo>)tokenCache;

                //cache里相同token值的用户信息
                List<UserInfo> cacheExistToken = users.FindAll(t => t.Token == iUserInfo.Token).ToList();

                if (cacheExistToken.Count > 0)
                {
                    //这个token已经存在了,告诉用户重新登录
                    return false;
                }

                users.Add(iUserInfo);
                HttpRuntime.Cache.Insert("Token", users);
            }
            return true;
        }

        /// <summary>
        /// 获取指定token的用户信息
        /// </summary>
        /// <param name="iToken"></param>
        /// <returns></returns>
        public static UserInfo GetUser(string iToken)
        {

            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            var tokenCache = objCache["Token"];

            if (tokenCache != null)
            {
                List<UserInfo> users = (List<UserInfo>)tokenCache;

                UserInfo user = users.FirstOrDefault<UserInfo>(t => t.Token == iToken);

                if (user != null && user.IsTimeOut() == false)
                {
                    user.UpdateVisitTime();
                    return user;
                }
            }

            #region 从数据库中获取登录信息


            #endregion

            return null;
        }

        public static void RemoveUser(string iToken)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            var tokenCache = objCache["Token"];

            if (tokenCache != null)
            {
                List<UserInfo> users = (List<UserInfo>)tokenCache;

                UserInfo user = users.FirstOrDefault<UserInfo>(t => t.Token == iToken);

                if (user != null)
                {
                    users.Remove(user);
                }
            }
        }
    }
}
