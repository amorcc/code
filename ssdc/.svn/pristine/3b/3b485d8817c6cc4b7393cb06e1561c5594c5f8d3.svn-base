using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SSDC.Common
{
    /// <summary>
    /// 生成3类医保的基础语句
    /// </summary>
    public class CreateBaseDataSql
    {

        /// <summary>
        /// 生成3类医保的基础insert 语句
        /// </summary>
        /// <param name="iType">1-城镇居民医保,2-城镇职工医保,3-新农合医保</param>
        /// <param name="iRow"></param>
        /// <param name="iNameColName"></param>
        /// <param name="iIdNumberColName"></param>
        /// <returns></returns>
        public static string CreatBaseDataInsertSql(int iType, System.Data.DataRow iRow, string iNameColName, string iIdNumberColName)
        {
            string tableName = "";

            if (iType >= 1 && iType <= 8)
            {
                tableName = "table" + iType;
            }
            else
            {
                throw new Exception("生成insert语句的类型不明确");
                return "";
            }

            string sqlStr = "";

            string name = string.Empty;
            string idNumber = string.Empty;
            ArrayList colList = new ArrayList();

            if (iRow != null)
            {
                for (int i = 0; i < iRow.Table.Columns.Count; i++)
                {
                    if (iRow.Table.Columns[i].ColumnName == iNameColName)
                    {
                        name = iRow[i].ToString();
                    }
                    else if (iRow.Table.Columns[i].ColumnName == iIdNumberColName)
                    {
                        idNumber = iRow[i].ToString();
                    }
                    else
                    {
                        colList.Add(iRow[i]);
                    }
                }
            }

            sqlStr = "INSERT INTO ssdc." + tableName + "(NAME,IdNumber,";

            if (colList != null && colList.Count > 0)
            {
                for (int i = 0; i < colList.Count; i++)
                {
                    int index = i + 1;
                    sqlStr += "col" + index.ToString() + ",";
                }
            }

            sqlStr = sqlStr.Substring(0, sqlStr.Length - 1);

            sqlStr += ") VALUES('" + name + "','" + idNumber + "',";

            if (colList != null && colList.Count > 0)
            {
                for (int i = 0; i < colList.Count; i++)
                {
                    sqlStr += "'" + colList[i].ToString() + "',";
                }
            }

            sqlStr = sqlStr.Substring(0, sqlStr.Length - 1);

            sqlStr += ");";

            return sqlStr;
        }

        public static string CreateBaseDataSelectSql(string iIdNumber, string iName)
        {
            string strSql = string.Format(@"SELECT  (SELECT dataName FROM datainfo WHERE id=a.TableNum) AS DataName,
                                              ID,
                                              NAME,
                                              IdNumber,
                                              col1,
                                              col2,
                                              col3,
                                              col4,
                                              col5,
                                              col6,
                                              col7,
                                              col8,
                                              col9,
                                              col10,
                                              col11,
                                              col12,
                                              col13,
                                              col14,
                                              col15,
                                              col16,
                                              col17,
                                              col18,
                                              col19,
                                              col20,
                                              col21,
                                              col22,
                                              col23,
                                              col24,
                                              col25,
                                              col26,
                                              col27,
                                              col28,
                                              col29,
                                              col30,
                                              DataAdd
                                            FROM user_data a ");
            //Where IdNumber='{0}'", iIdNumber);

            if (!string.IsNullOrEmpty(iIdNumber.Trim()) && !string.IsNullOrEmpty(iName.Trim()))
            {
                strSql += string.Format("Where IdNumber='{0}' and Name='{1}'", iIdNumber, iName);
            }
            else if (!string.IsNullOrEmpty(iIdNumber.Trim()) && string.IsNullOrEmpty(iName.Trim()))
            {
                strSql += string.Format("Where IdNumber='{0}' ", iIdNumber);
            }
            else if (string.IsNullOrEmpty(iIdNumber.Trim()) && !string.IsNullOrEmpty(iName.Trim()))
            {
                strSql += string.Format("Where Name='{0}' ", iName);
            }
            else
            {
                strSql += " limit 1,15 ";
            }

            return strSql;
        }

        public static string CreateBaseDataSelectAllSql(int iType)
        {
            string strSql = string.Format(@"SELECT (SELECT dataName FROM datainfo WHERE id={0}) AS DataName,
                                              ID,
                                              NAME,
                                              IdNumber,
                                              col1,
                                              col2,
                                              col3,
                                              col4,
                                              col5,
                                              col6,
                                              col7,
                                              col8,
                                              col9,
                                              col10,
                                              col11,
                                              col12,
                                              col13,
                                              col14,
                                              col15,
                                              col16,
                                              col17,
                                              col18,
                                              col19,
                                              col20,
                                              col21,
                                              col22,
                                              col23,
                                              col24,
                                              col25,
                                              col26,
                                              col27,
                                              col28,
                                              col29,
                                              col30,
                                              DataAdd
                                            FROM table{0} LIMIT 1,1000", iType);
            return strSql;
        }
    }
}
