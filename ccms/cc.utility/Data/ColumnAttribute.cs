using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.utility.Data
{
    /// <summary>
    ///  指定该属性是否是Table中的主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 是否是主键
        /// </summary>
        private bool _isprimary;
        /// <summary>
        /// 是否自增长
        /// </summary>
        private bool _isidentity;

        private bool _isNotNull;

        private int _varcharLength;

        /// <summary>
        /// 是否序列化
        /// </summary>
        private bool _isSerialize = true;

        /// <summary>
        /// 是否序列化
        /// </summary>
        public bool IsSerialize
        {
            get { return _isSerialize; }
            set { _isSerialize = value; }
        }

        /// <summary>
        /// 是否是数据库字段
        /// </summary>
        private bool _isTableColumn = true;

        /// <summary>
        /// 是否是数据库字段
        /// </summary>
        public bool IsTableColumn
        {
            get { return _isTableColumn; }
            set { _isTableColumn = value; }
        }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimary
        {
            get { return _isprimary; }
            set { _isprimary = value; }
        }
        /// <summary>
        /// 是否自增长
        /// </summary>
        public bool IsIdentity
        {
            get { return _isidentity; }
            set { _isidentity = value; }
        }

        /// <summary>
        /// 是否不允许为空
        /// </summary>
        public bool IsNotNull
        {
            get { return _isNotNull; }
            set { _isNotNull = value; }
        }


        public int VarcharLength
        {
            get { return _varcharLength; }
            set { _varcharLength = value; }
        }

        public ColumnAttribute()
        {
            this.IsIdentity = false;
            this.IsPrimary = false;
            this.IsNotNull = false;
            this.IsSerialize = true;
            this.IsTableColumn = true;
            this.VarcharLength = 200;
        }

    }
}
