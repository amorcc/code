using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SSDC.DAL
{
    public class datainfo
    {
        public DataSet GetDataInfo()
        {
            string strSql = "SELECT ID,DataName,RowCount,ModifyTime,TableName FROM datainfo ORDER BY ID;";
            //string strSql = "SELECT ID,DataName,(SELECT COUNT(0) FROM user_data a WHERE a.TableNum=t.ID) AS RowCount,ModifyTime,TableName FROM datainfo t ORDER BY ID;";

            return SSDC.Common.DirectDbHelperMysql.Query(strSql);
        }

        public static DataTable GetAll()
        {
            string strSql = "SELECT ID,DataName FROM datainfo";
            //string strSql = "SELECT ID,DataName,(SELECT COUNT(0) FROM user_data a WHERE a.TableNum=t.ID) AS RowCount,ModifyTime,TableName FROM datainfo t ORDER BY ID;";

            return SSDC.Common.DirectDbHelperMysql.Query(strSql).Tables[0];
        }


        public static void SetDataInfoColumnsNum(int iTableNum)
        {
            string strSql = string.Format("CALL UpdateDataInfo({0});", iTableNum);

            SSDC.Common.DirectDbHelperMysql.ExecuteSql(strSql);
        }

        public static void InsertByDataRow(System.Data.DataRow iRow, string iNameColName, string iIdNumberColName)
        {
            string sqlStr = SSDC.Common.CreateBaseDataSql.CreatBaseDataInsertSql(1, iRow, iNameColName, iIdNumberColName);

            SSDC.Common.DirectDbHelperMysql.ExecuteSql(sqlStr);
        }

        public static void UpdateDataName(int iID, string iDataName)
        {
            string sqlStr = @"UPDATE datainfo SET DataName=@dataName WHERE ID = @id;";

            var para = new[]{
                new MySqlParameter("@dataName",iDataName),
                new MySqlParameter("@id",iID),
            };

            SSDC.Common.DirectDbHelperMysql.ExecuteSql(sqlStr, para);
        }
    }
}
