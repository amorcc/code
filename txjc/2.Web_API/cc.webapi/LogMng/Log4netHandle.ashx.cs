using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UC_WebAPI
{
    /// <summary>
    /// Log4netHandle 的摘要说明
    /// </summary>
    public class Log4netHandle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            cc.log.ReadLog4net readLog4net = new cc.log.ReadLog4net();
            context.Response.ContentType = "text/json";
            context.Response.Charset = "UTF-8";

            string result = string.Empty;

            try
            {
                string serverCmd = context.Request["cmd"];
                string para = context.Request["para"];

                switch (serverCmd)
                {
                    case "FileList":
                        result = readLog4net.GetLogFileList();
                        break;
                    case "ReadFile":
                        result = readLog4net.GetLogFileInfo(para);
                        break;
                    default:
                        break;
                }

                context.Response.Write(result);
            }
            catch (System.Exception ex)
            {
                if (!(ex is System.Threading.ThreadAbortException))
                {
                    string msg = ex.Message;
                    context.Response.Write(msg);
                }
            }
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}