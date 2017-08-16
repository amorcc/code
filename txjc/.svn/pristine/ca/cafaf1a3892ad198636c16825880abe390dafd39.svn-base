using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cc.core
{

    public class StandardPayload:MyEntity
    {
        public StandardPayload()
        {
            ResponseID = 0;
            Message = "";
            Data = JObject.Parse("{}");
        }
        /// <summary>
        /// 返回的 状态代码 0 代表成功, 其他 含义 待定
        /// </summary>
        public int ResponseID { get; set; }
        /// <summary>
        /// 对应 状态代码的 消息.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 用户自己的 返回数据,建议用 json string
        /// </summary>
        public object Data { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
