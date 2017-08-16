using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.basemodel
{
    public class StandardPayload
    {
        public int ResponseID
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public object Data
        {
            get;
            set;
        }

        public StandardPayload()
        {
            this.ResponseID = 0;
            this.Message = "";
            this.Data = JObject.Parse("{}");
        }
    }
}
