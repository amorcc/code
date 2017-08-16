using System;
using System.Collections.Generic;
using System.Text;

namespace SSDC.DAL
{
    /// <summary>
    /// 新农合医保
    /// </summary>
    public class xnhyb
    {
        public static void InsertByDataRow(System.Data.DataRow iRow, string iNameColName, string iIdNumberColName)
        {
            string sqlStr = SSDC.Common.CreateBaseDataSql.CreatBaseDataInsertSql(3, iRow, iNameColName, iIdNumberColName);

            SSDC.Common.DirectDbHelperMysql.ExecuteSql(sqlStr);
        }
    }
}
