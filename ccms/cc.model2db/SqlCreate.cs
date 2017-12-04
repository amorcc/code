using cc.utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.model2db
{
    public class ColumnData
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColName;

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimary;

        /// <summary>
        /// 是否自增长
        /// </summary>
        public bool IsIdentity;

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColDataType;

        /// <summary>
        /// 如果是varchar的话，它的长度
        /// </summary>
        public int VarcharLength;

        /// <summary>
        /// 字段注释
        /// </summary>
        public string ColDesc;

        /// <summary>
        /// 是否不允许为null
        /// </summary>
        public bool IsNotNull;

        public string DBDataType
        {
            get
            {
                switch (this.ColDataType)
                {
                    case "System.Int32":
                    case "System.Int64":
                        return "[int]";
                    case "System.DateTime":
                        return "[datetime]";
                    default:
                        return string.Format("[varchar]({0})", this.VarcharLength.ToString());
                }
            }
        }

        public ColumnData(string iColName, bool iIsPrimary, bool iIsIdentity, bool iIsNotNull, string
            iColDataType, int iVarcharLength, string iColDesc)
        {
            this.ColName = iColName;
            this.IsPrimary = iIsPrimary;
            this.IsIdentity = iIsIdentity;
            this.IsNotNull = iIsNotNull;
            this.ColDataType = iColDataType;
            this.VarcharLength = iVarcharLength;
            this.ColDesc = iColDesc;
        }

    }

    /// <summary>
    /// 生成sql语句
    /// </summary>
    public class SqlCreate
    {
        public static string GetSql(string iTableName, List<ColumnData> iCols, bool iDBExist, List<DBTableColumn> iDBTableColList)
        {
            string rt = string.Empty;

            if (iDBExist == false)
            {
                return GetCreateSql(iTableName, iCols);
            }
            else
            {
                return GetAlterSql(iTableName, iCols, iDBTableColList);
            }
        }

        static string GetCreateSql(string iTableName, List<ColumnData> iCols)
        {
            string rt = string.Empty;
            string primaryStr = string.Empty;

            rt += string.Format("CREATE TABLE [dbo].[{0}](", iTableName);

            foreach (var col in iCols)
            {
                string type = col.DBDataType;
                string identity = col.IsIdentity ? "IDENTITY(1,1)" : "";
                string notNull = col.IsNotNull ? "NOT NULL" : "NULL";

                if (col.IsPrimary)
                {
                    notNull = "NOT NULL";
                }

                rt += string.Format("      [{0}] {1} {2} {3},", col.ColName, type, identity, notNull);

                if (col.IsPrimary)
                {
                    primaryStr += string.IsNullOrEmpty(primaryStr) ? "" : ",";
                    primaryStr += string.Format("[{0}] ASC", col.ColName);
                }
            }

            rt += string.Format("      PRIMARY KEY CLUSTERED ( {0} )", primaryStr);
            rt += string.Format("        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,");
            rt += string.Format("               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,");
            rt += string.Format("               ALLOW_PAGE_LOCKS = ON )");

            rt += string.Format(");");

            return rt;
        }

        static string GetAlterSql(string iTableName, List<ColumnData> iCols, List<DBTableColumn> iDBTableColList)
        {
            string rt = string.Empty;

            foreach (var col in iCols)
            {
                string type = col.DBDataType;
                string identity = col.IsIdentity ? "IDENTITY(1,1)" : "";
                string notNull = col.IsNotNull ? "NOT NULL" : "NULL";

                if (col.IsPrimary)
                {
                    notNull = "NOT NULL";
                }

                bool exist = (from t in iDBTableColList
                              where t.ColumnName == col.ColName
                              select t).Count() > 0 ? true : false;

                if (col.IsPrimary == false || (col.IsPrimary == true && exist == false))
                {
                    rt += string.Format("ALTER TABLE [{0}] {4} [{1}] {2} {3};", iTableName, col.ColName, type, notNull, exist ? "ALTER COLUMN " : "ADD");
                }

            }

            return rt;
        }

    }
}
