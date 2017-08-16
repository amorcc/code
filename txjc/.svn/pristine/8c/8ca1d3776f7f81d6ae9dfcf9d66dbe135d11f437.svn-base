using cc.common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.log
{
    public class Log
    {
        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iUserName"></param>
        /// <param name="iRoleId"></param>
        /// <param name="iSiteId"></param>
        /// <param name="iResponseID"></param>
        /// <param name="iIp"></param>
        /// <param name="iMisc"></param>
        /// <param name="iPartnerId"></param>
        /// <param name="iDesc"></param>
        public static void LoginLog(string iUserName, int iRoleId, int iSiteId, int iResponseID, string iIp, string iMisc, string iPartnerId, string iDesc = "")
        {
            JObject para = new JObject()
            {
                {"username", iUserName},
                {"roleid", iRoleId},
                {"siteid", iSiteId},
                {"responseid", iResponseID},
                {"ip", iIp},
                {"misc", iMisc},
                {"partnerid", iPartnerId},
                {"desc", iDesc},
            };

            //AddLog4netLog(typeof(Log), para);
            LogPool.EnQueue(LogType.LoginLog, para);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="iType">class的Type:typeof(classname)</param>
        /// <param name="iSiteId">当前报错的站点：(int)UserTokenManage.Site.UC</param>
        /// <param name="iException">异常信息</param>
        /// <param name="iNodeName">其他信息</param>
        public static void Error(Type iType, Exception iException, string iNodeName = "")
        {
            AddLogToSentinel(iType, BLLogType.ERROR, 0, "", 0, 0, "", "", iException.ToString(), iNodeName, "", "", "", "");
        }

        /// <summary>
        /// 添加调试信息
        /// </summary>
        /// <param name="iType">class的Type:typeof(classname)</param>
        /// <param name="iSiteId">当前报错的站点：(int)UserTokenManage.Site.UC</param>
        /// <param name="iUserInfo">用户登录信息</param>
        /// <param name="iDebugInfo">DEBUG信息</param>
        /// <param name="iBusinessCode">业务代码</param>
        /// <param name="iNodeName">业务节点名称</param>
        /// <param name="iParaIN">输入参数</param>
        /// <param name="iParaOut">输出参数</param>
        public static void Debug(Type iType, UserInfo iUserInfo, string iDebugInfo, string iBusinessCode = "", string iNodeName = "", string iParaIN = "无", string iParaOut = "无")
        {
            int userId = 0;
            int roleId = 0;
            string userSN = "";

            if (iUserInfo != null)
            {
                userId = iUserInfo.UserId;
                userSN = iUserInfo.UserSN;
            }

            AddLogToSentinel(iType, BLLogType.DEBUG, userId, userSN, roleId, 0, "", "", iDebugInfo, iNodeName, iBusinessCode, iParaIN, iParaOut, "");
        }

        /// <summary>
        /// 订单日志
        /// </summary>
        /// <param name="iType">class的Type:typeof(classname)</param>
        /// <param name="iSiteId">当前报错的站点：(int)UserTokenManage.Site.UC</param>
        /// <param name="iUserInfo">用户登录信息</param>
        /// <param name="iOrderInfo">订单信息</param>
        /// <param name="iBatchId">大订单号</param>
        /// <param name="iOrderCode">订单号</param>
        /// <param name="iBusinessCode">业务代码</param>
        /// <param name="iNodeName">业务节点名称</param>
        /// <param name="iParaIN">输入参数</param>
        /// <param name="iParaOut">输出参数</param>
        public static void OrderLog(Type iType, int iSiteId, UserInfo iUserInfo, string iOrderInfo, string iBatchId, string iOrderCode, string iBusinessCode = "", string iNodeName = "", string iParaIN = "无", string iParaOut = "无")
        {
            int userId = 0;
            int roleId = 0;
            string userSN = "";

            if (iUserInfo != null)
            {
                userId = iUserInfo.UserId;
                userSN = iUserInfo.UserSN;
            }

            AddLogToSentinel(iType, BLLogType.OrderLog, userId, userSN, roleId, iSiteId, iBatchId, iOrderCode, iOrderInfo, iNodeName, iBusinessCode, iParaIN, iParaOut, "");
        }



        static void AddLogToSentinel(Type iType, BLLogType iBLLogType, int iUserId, string iUserSN, int iRoleId,
            int iSiteId, string iBatchId, string iOrderCode, string iDescription, string iNodeName,
            string iBusinessCode, string iParaIn, string iParaOut, string iIP)
        {
            JObject para = new JObject()
            {
                {"logtype", (int)iBLLogType},
                {"userid", iUserId},
                {"usersn", iUserSN},
                {"roleid", iRoleId},
                {"siteid", iSiteId},
                {"batchid", iBatchId},
                {"ordercode", iOrderCode},
                {"description", iDescription},
                {"nodename", RemoveEnterChar(iNodeName)},
                {"businesscode", iBusinessCode},
                {"iparas", iParaIn},
                {"oparas", iParaOut},
                {"ip",  iIP},
                {"classinfo",  iType.ToString()},
            };

            //通过log4net写日志
            AddLog4netLog(iType, para);

            LogPool.EnQueue(LogType.BusinessLogicLog, para);
        }

        /// <summary>
        /// 写日志到log4net
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iPara"></param>
        public static void AddLog4netLog(Type iType, JObject iPara)
        {
            string para = iPara.ToString();
            para = RemoveEnterChar(para);
            log4net.LogManager.GetLogger(iType).Debug(para);
        }

        public static void AddLog4netLog(Type iType, Exception iEx)
        {
            JObject jo = new JObject()
            {
                {"logtype", (int)BLLogType.ERROR},
                {"userid", 0},
                {"usersn", ""},
                {"roleid", ""},
                {"siteid", 1},
                {"batchid", ""},
                {"ordercode", ""},
                {"description", iEx.ToString()},
                {"nodename", "哨兵队列错误"},
                {"businesscode", ""},
                {"iparas", ""},
                {"oparas", ""},
                {"ip",  ""},
                {"classinfo",  typeof(LogPool).ToString()},
            };

            Log.AddLog4netLog(iType, jo);
        }

        /// <summary>
        /// 过滤回车
        /// </summary>
        /// <param name="iStr"></param>
        /// <returns></returns>
        private static string RemoveEnterChar(string iStr)
        {
            if (!string.IsNullOrEmpty(iStr))
            {
                iStr = iStr.Replace("\r", "");
                iStr = iStr.Replace("\n", "");
            }

            return iStr;
        }

        public static bool IsLoadedLog4net()
        {
            if (log4net.LogManager.GetLogger(typeof(string)).IsErrorEnabled == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
