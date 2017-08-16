using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SSDC.DAL
{
    /// <summary>
    /// 分析结果
    /// </summary>
    public class AnalysisResult
    {

        /// <summary>
        /// 返回重复信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRepeatInfo()
        {
            string strSql = "SELECT * FROM view_repeatidnumber";

            return SSDC.Common.DirectDbHelperMysql.Query(strSql).Tables[0];
        }
    }
}
