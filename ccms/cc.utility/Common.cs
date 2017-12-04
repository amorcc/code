using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.utility
{
    public class Common
    {
        public static string App(string key)
        {
            return ConfigurationManager.AppSettings[key] == null ? "" : ConfigurationManager.AppSettings[key].ToString();
        }

        public static string ConnectionStrings(string key)
        {
            return ConfigurationManager.ConnectionStrings[key] == null ? "" : ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}
