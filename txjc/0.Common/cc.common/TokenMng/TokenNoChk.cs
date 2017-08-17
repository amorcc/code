using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.common.TokenMng
{
    public class TokenNoChk
    {
        static List<string> ACTION_NO_CHECK_LIST = new List<string>();

        static TokenNoChk()
        {
            ACTION_NO_CHECK_LIST.Add("Login");
            ACTION_NO_CHECK_LIST.Add("IsLogin");
            ACTION_NO_CHECK_LIST.Add("GetCartNum");
            ACTION_NO_CHECK_LIST.Add("GetAreaInfo");
            ACTION_NO_CHECK_LIST.Add("Upload");
            ACTION_NO_CHECK_LIST.Add("Reg");
            ACTION_NO_CHECK_LIST.Add("GetOpenId");
            ACTION_NO_CHECK_LIST.Add("CreateQCcode");
            ACTION_NO_CHECK_LIST.Add("JoinMe");
            ACTION_NO_CHECK_LIST.Add("SendToWxOrderCreateMsg");
            ACTION_NO_CHECK_LIST.Add("GetWxJsApiParam");
        }

        /// <summary>
        /// 判断是否需要验证Token
        /// </summary>
        /// <param name="iActionName"></param>
        /// <returns></returns>
        public static bool IsTonkenNoCheck(string iActionName)
        {
            if (ACTION_NO_CHECK_LIST != null)
            {
                var actionNameCount = (from t in ACTION_NO_CHECK_LIST
                                       where t == iActionName
                                       select t).Count();

                if (actionNameCount > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
