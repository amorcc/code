using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.common.Sys
{
    /// <summary>
    /// 系统连接字符串管理
    /// </summary>
    public class SystemConnections
    {
        public static string B2bConn
        {
            get { return ConfigurationManager.ConnectionStrings["B2B_DB"].ConnectionString; }
        }
    }
}
