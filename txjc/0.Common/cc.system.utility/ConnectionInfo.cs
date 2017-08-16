using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.system.utility
{
    public class SystemConnections
    {
        public static string UCConn
        {
            get { return ConfigurationManager.ConnectionStrings["ucDB"].ConnectionString; }
        }

        /// <summary>
        /// 产品库
        /// </summary>
        public static string SKUConn
        {
            get { return ConfigurationManager.ConnectionStrings["skuDB"].ConnectionString; }
        }
        /// <summary>
        /// 串号库
        /// </summary>
        public static string ImeiConn
        {
            get { return ConfigurationManager.ConnectionStrings["imeiDB"].ConnectionString; }
        }
        /// <summary>
        /// 认证库
        /// </summary>
        public static string AuthConn
        {
            get { return ConfigurationManager.ConnectionStrings["authDB"].ConnectionString; }
        }
        /// <summary>
        /// 进销存
        /// </summary>
        public static string PSIConn
        {
            get { return ConfigurationManager.ConnectionStrings["psiDB"].ConnectionString; }
        }
        /// <summary>
        /// B2b
        /// </summary>
        public static string B2bConn
        {
            get { return ConfigurationManager.ConnectionStrings["B2bDB"].ConnectionString; }
        }

        public static string SentinelConn
        {
            get { return ConfigurationManager.ConnectionStrings["SentinelDB"].ConnectionString; }
        }


        /// <summary>
        /// Sql Injection 关键字过滤列表所在的文件名
        /// </summary>
        public static string SqlInjection
        {
            get
            {
                var fileName = "sqlInjection.txt";
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["sqlInjection"]))
                {
                    fileName = ConfigurationManager.AppSettings["sqlInjection"];
                }
                return fileName;
            }
        }



    }
}
