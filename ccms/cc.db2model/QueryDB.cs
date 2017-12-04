using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using cc.utility;

namespace cc.db2model
{
    public class QueryDB
    {
        public static List<string> GetDBConnStr()
        {
            List<string> rt = new List<string>();
            if (ConfigurationManager.ConnectionStrings.Count > 0)
            {
                for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
                {
                    string name = ConfigurationManager.ConnectionStrings[i].Name;

                    if (name != "LocalSqlServer" && name != "LocalMySqlServer")
                    {
                        rt.Add(name);
                    }
                }
            }

            return rt;
        }

        public static List<DBTableColumn> GetTableColumnInfo(string iDBConnName, string iTableName)
        {
            List<DBTableColumn> rt = new List<DBTableColumn>();
            string connStr = cc.utility.Common.ConnectionStrings(iDBConnName);
            string sql = string.Format(@"SELECT  t.[name] AS TableName ,
                                                c.[name] AS ColumnName ,
                                                CAST(ep.[value] AS VARCHAR(100)) AS ColumnDesc ,
                                                d.name AS ColumnType
                                        FROM    sys.tables AS t
                                                INNER JOIN sys.columns AS c ON t.object_id = c.object_id
                                                INNER JOIN sys.systypes AS d ON c.system_type_id = d.xtype
                                                LEFT JOIN sys.extended_properties AS ep ON ep.major_id = c.object_id
                                                                                           AND ep.minor_id = c.column_id
                                        WHERE   t.name = '{0}';", iTableName);

            using (SqlDataReader dr = SqlHelper.ExecuteReader(connStr, CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    string colName = DataConvert.ToString(dr["ColumnName"]);
                    string colDesc = DataConvert.ToString(dr["ColumnDesc"]);
                    Type colType = DBTableColumn.GetColType(DataConvert.ToString(dr["ColumnType"]));

                    rt.Add(new DBTableColumn(colName, colDesc, colType));
                }

                dr.Close();
            }

            return rt;
        }

        /// <summary>
        /// 获取数据库中的所有表名
        /// </summary>
        /// <param name="iDBConnName"></param>
        /// <returns></returns>
        public static List<string> GetTableNameListFromDB(string iDBConnName)
        {
            List<string> tableNames = new List<string>();

            //string sql = @"SELECT Name FROM SysObjects Where XType='U' OR XType='V' ORDER BY Name ";
            string sql = @"SELECT Name FROM SysObjects Where XType='U' ORDER BY Name ";

            string connStr = cc.utility.Common.ConnectionStrings(iDBConnName);

            using (SqlDataReader dr = SqlHelper.ExecuteReader(connStr, CommandType.Text, sql, null))
            {
                while (dr.Read())
                {
                    tableNames.Add(dr["Name"].ToString());
                }

                dr.Close();
            }

            return tableNames;
        }
    }

    public class DBTableColumn
    {
        public string ColumnName;
        public Type ColumsType;
        public string ColumnDesc;

        public DBTableColumn(string iColName, string iColDesc, Type iColType)
        {
            this.ColumnDesc = iColDesc;
            this.ColumnName = iColName;
            this.ColumsType = iColType;
        }

        public static Type GetColType(string iDBType)
        {
            switch (iDBType)
            {
                case "int":
                    return typeof(int);
                case "decimal":
                    return typeof(decimal);
                case "varchar":
                    return typeof(string);
                case "bigint":
                    return typeof(Int64);
                case "datetime":
                case "smalldatetime":
                    return typeof(DateTime);
                case "tinyint":
                    return typeof(int);
                case "smallint":
                    return typeof(int);
                default:
                    throw new Exception(string.Format("数据库列类型{0}未处理", iDBType));
            }
        }

        public static string GetCSharpTypeStr(Type iType)
        {
            switch (iType.ToString())
            {
                case "System.String":
                    return "string";
                case "System.Int32":
                    return "int";
                case "System.Int64":
                    return "Int64";
                case "System.DateTime":
                    return "DateTime";
                case "System.Boolean":
                    return "bool";
                case "System.Decimal":
                    return "Decimal";
                default:
                    return iType.ToString();
            }

        }

        public static string GetCSharpDataConvert(Type iType)
        {
            switch (iType.ToString())
            {
                case "System.String":
                    return "ToString";
                case "System.Int32":
                    return "ToInt32";
                case "System.Int64":
                    return "ToInt64";
                case "System.DateTime":
                    return "ToDateTime";
                case "System.Boolean":
                    return "ToInt32";
                case "System.Decimal":
                    return "ToDecimal";
                default:
                    return iType.ToString();
            }
        }
    }
}
