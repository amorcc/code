using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.utility;
using Newtonsoft.Json.Linq;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            JObject t = new JObject()
            {
                {"count",""},
            };

            int a = t.GetValueExt<int>("count");


        }
    }
}
