using System;
using System.Net;

namespace ESFT.Common.Log
{
    public class MyLogManage
    {
        public static void Debug(Type iType, string iFunctionName, EndPoint iClientEndPoint, string iDebugInfo)
        {
            string debugInfo = " func = " + iFunctionName;
            if (iClientEndPoint != null)
            {
                debugInfo += " , ClientIp = '" + iClientEndPoint.ToString() + "'";
            }
            debugInfo += " , iDebugInfo = '" + iDebugInfo + "'";
            log4net.LogManager.GetLogger(iType).Debug(debugInfo);
        }

        public static void Debug(string iCSFile, string iFunctionName, string iDebugInfo)
        {
            string debugInfo = " func = " + iFunctionName
                + " , iDebugInfo = '" + iDebugInfo + "'";
            log4net.LogManager.GetLogger(iCSFile).Debug(debugInfo);
        }
    }
}
