using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SSDC.DAL
{
    /// <summary>
    /// 城镇居民医保
    /// </summary>
    public class czjmyb
    {
        public static void InsertByDataRow(int iDatainfoType, System.Data.DataRow iRow, string iNameColName, string iIdNumberColName)
        {
            string sqlStr = SSDC.Common.CreateBaseDataSql.CreatBaseDataInsertSql(iDatainfoType, iRow, iNameColName, iIdNumberColName);

            SSDC.Common.DirectDbHelperMysql.ExecuteSql(sqlStr);
        }


    }
}
