using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.common.Sys
{
    public class AppMng
    {
        public static string App(string key)
        {
            return ConfigurationManager.AppSettings[key] == null ? "" : ConfigurationManager.AppSettings[key].ToString();
        }
    }
}
