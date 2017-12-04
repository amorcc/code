using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.utility.Data
{
    /// <summary>
    /// 指定Model所对应的Table名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 表的名称
        /// </summary>
        private string _tablename = string.Empty;
        /// <summary>
        /// 映射为表的名称
        /// </summary>
        public string TableName
        {
            get { return _tablename; }
            set { _tablename = value; }
        }
        public TableAttribute()
        {
        }
    }
}
