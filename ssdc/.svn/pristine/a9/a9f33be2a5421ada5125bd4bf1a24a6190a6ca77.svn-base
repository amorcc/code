using System;
using System.Collections.Generic;
using System.Text;

namespace SSDC.DAL
{
    /// <summary>
    /// 城镇职工医保
    /// </summary>
    public class czzzyb
    {
        public static void InsertByDataRow(System.Data.DataRow iRow, string iNameColName, string iIdNumberColName)
        {
            string sqlStr = SSDC.Common.CreateBaseDataSql.CreatBaseDataInsertSql(2, iRow, iNameColName, iIdNumberColName);

            SSDC.Common.DirectDbHelperMysql.ExecuteSql(sqlStr);
        }
    }
}
