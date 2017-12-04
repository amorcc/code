using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.utility.Data;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace cc.basemodel
{
    public class BaseModel
    {
        public BaseModel()
        {
            DateAdded = DateTime.Now;
        }

        /// <summary>
        /// 主键，默认必须有
        /// </summary>
        [System.ComponentModel.Description("主键")]
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 添加数据的时间
        /// </summary>
        [System.ComponentModel.Description("添加时间")]
        public DateTime DateAdded { get; set; }

        public JObject ToJOject()
        {
            JObject jo = new JObject();
            Type t = this.GetType();
            PropertyInfo[] pis = t.GetProperties();

            foreach (PropertyInfo pi in pis)
            {
            }

            return jo;
        }
    }
}
