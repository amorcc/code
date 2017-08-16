using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SSDC.DAL
{
    /// <summary>
    /// 数据导入记录
    /// </summary>
    public class ImportRecord
    {
        /// <summary>
        /// 新增导入记录信息
        /// </summary>
        /// <param name="iImportCount">导入总条数</param>
        /// <param name="iAddBatch">批次唯一标识</param>
        public static void Insert(int iImportCount, DateTime iAddDate, string iAddBatch, int iTableNum)
        {
            string desc = string.Empty;

            string strSql = string.Empty;

            strSql += "INSERT INTO ssdc.importrecord(ImportCount,DataAdd,AddBatch,TableNum)";
            strSql += "	VALUES";
            strSql += "	(" + iImportCount.ToString() + ", ";
            strSql += "	STR_TO_DATE('" + iAddDate.ToString("yyyy-MM-dd HH:mm:ss") + "','%Y-%m-%d %H:%i:%s'), ";
            strSql += "	'" + iAddBatch + "'," + iTableNum;
            strSql += "	);";

            Common.DirectDbHelperMysql.ExecuteSql(strSql);
        }

        public static void DeleteTable(int iTableNum)
        {
            string strSql = string.Format("Call DeleteDataInfo({0});", iTableNum);

            SSDC.Common.DirectDbHelperMysql.ExecuteSql(strSql);
        }

        public static DataTable GetAll()
        {
            string strSql = "select * from view_importrecord";

            return Common.DirectDbHelperMysql.Query(strSql).Tables[0];
        }
    }

}
