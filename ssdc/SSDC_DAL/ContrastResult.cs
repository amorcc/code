using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SSDC.DAL
{
    public class ContrastResult
    {
        static DataTable ModifyColumnName(DataTable iDt)
        {
            DataTable dataNameTable = SSDC.DAL.datainfo.GetAll();
            if (iDt != null)
            {
                for (int i = 0; i < iDt.Columns.Count; i++)
                {
                    string colName = iDt.Columns[i].ColumnName;
                    int id = 0;

                    if (colName == "table1count")
                    {
                        id = 1;
                    }

                    switch (colName)
                    {
                        case "table1count":
                            id = 1;
                            break;
                        case "table2count":
                            id = 2;
                            break;
                        case "table3count":
                            id = 3;
                            break;
                        case "table4count":
                            id = 4;
                            break;
                        case "table5count":
                            id = 5;
                            break;
                        case "table6count":
                            id = 6;
                            break;
                        case "table7count":
                            id = 7;
                            break;
                        case "table8count":
                            id = 8;
                            break;
                        default:
                            id = 0;
                            break;
                    }

                    if (id >= 1 && id <= 8)
                    {
                        var rows = dataNameTable.Select("ID=" + id);

                        if (rows != null && rows.Length > 0)
                        {
                            iDt.Columns[i].ColumnName = rows[0]["DataName"].ToString();
                        }
                    }
                }
            }

            return iDt;
        }

        static DataTable ModifyColumnName2(DataTable iDt)
        {
            DataTable dataNameTable = SSDC.DAL.datainfo.GetAll();
            if (iDt != null)
            {
                for (int i = 0; i < iDt.Columns.Count; i++)
                {
                    string colName = iDt.Columns[i].ColumnName;
                    int id = 0;

                    switch (colName)
                    {
                        case "table1count":
                        case "table1IdNumber":
                        case "table1col1":
                        case "table1col2":
                        case "table1col3":
                        case "table1col4":
                        case "table1col5":
                        case "table1col6":
                        case "table1col7":
                        case "table1col8":
                            id = 1;
                            break;
                        case "table2count":
                        case "table2IdNumber":
                        case "table2col1":
                        case "table2col2":
                        case "table2col3":
                        case "table2col4":
                        case "table2col5":
                        case "table2col6":
                        case "table2col7":
                        case "table2col8":

                            id = 2;
                            break;
                        case "table3count":
                        case "table3IdNumber":
                        case "table3col1":
                        case "table3col2":
                        case "table3col3":
                        case "table3col4":
                        case "table3col5":
                        case "table3col6":
                        case "table3col7":
                        case "table3col8":

                            id = 3;
                            break;
                        case "table4count":
                            id = 4;
                            break;
                        case "table5count":
                            id = 5;
                            break;
                        case "table6count":
                            id = 6;
                            break;
                        case "table7count":
                            id = 7;
                            break;
                        case "table8count":
                            id = 8;
                            break;
                        default:
                            id = 0;
                            break;
                    }

                    if (id >= 1 && id <= 8)
                    {
                        var rows = dataNameTable.Select("ID=" + id);

                        if (rows != null && rows.Length > 0)
                        {
                            string dataName = rows[0]["DataName"].ToString();

                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table1IdNumber", dataName + "身份证号");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table2IdNumber", dataName + "身份证号");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table3IdNumber", dataName + "身份证号");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table1col1", dataName + "列1");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table1col2", dataName + "列2");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table1col3", dataName + "列3");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table1col4", dataName + "列4");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table1col5", dataName + "列5");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table1col6", dataName + "列6");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table1col7", dataName + "列7");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table1col8", dataName + "列8");

                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table2col1", dataName + "列1");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table2col2", dataName + "列2");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table2col3", dataName + "列3");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table2col4", dataName + "列4");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table2col5", dataName + "列5");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table2col6", dataName + "列6");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table2col7", dataName + "列7");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table2col8", dataName + "列8");

                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table3col1", dataName + "列1");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table3col2", dataName + "列2");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table3col3", dataName + "列3");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table3col4", dataName + "列4");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table3col5", dataName + "列5");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table3col6", dataName + "列6");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table3col7", dataName + "列7");
                            iDt.Columns[i].ColumnName = iDt.Columns[i].ColumnName.Replace("table3col8", dataName + "列8");

                        }
                    }
                }
            }

            return iDt;
        }
        #region 总体情况
        public static DataTable ResultAll(int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            string sqlStr = string.Format("CALL ResultAll({0},{1})", iPageIndex, iPageSize);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return ModifyColumnName(data);
        }
        #endregion

        #region 居民+职工+新农合
        public static DataTable Result123(int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            string sqlStr = string.Format("CALL Result123({0},{1})", iPageIndex, iPageSize);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return ModifyColumnName2(data);
        }
        #endregion

        #region 居民+职工.
        public static DataTable Result12(int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            string sqlStr = string.Format("CALL Result12({0},{1})", iPageIndex, iPageSize);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return ModifyColumnName2(data);
        }
        #endregion

        #region 居民+新农合
        public static DataTable Result13(int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            string sqlStr = string.Format("CALL Result13({0},{1})", iPageIndex, iPageSize);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return ModifyColumnName2(data);
        }
        #endregion

        #region 职工+新农合
        public static DataTable Result23(int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            string sqlStr = string.Format("CALL Result23({0},{1})", iPageIndex, iPageSize);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return ModifyColumnName2(data);
        }
        #endregion

        #region 居民
        public static DataTable Result1(int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            string sqlStr = string.Format("CALL Result1({0},{1})", iPageIndex, iPageSize);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return ModifyColumnName2(data);
        }
        #endregion

        #region 职工
        public static DataTable Result2(int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            string sqlStr = string.Format("CALL Result2({0},{1})", iPageIndex, iPageSize);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return ModifyColumnName2(data);
        }
        #endregion

        #region 新农合
        public static DataTable Result3(int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            string sqlStr = string.Format("CALL Result3({0},{1})", iPageIndex, iPageSize);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return ModifyColumnName2(data);
        }
        #endregion

        #region 身份证非法

        public static DataTable ResultIdNumber(int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            string sqlStr = string.Format("CALL ResultIdNumber({0},{1})", iPageIndex, iPageSize);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return data;
        }
        #endregion

        /// <summary>
        /// 读取数据库中的分析结果
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRepeatInfo()
        {
            string strSql = "SELECT * FROM view_ContrastResult WHERE table1count+table2count+table3count+table4count+table5count+table6count+table7count+table8count > 1";

            return SSDC.Common.DirectDbHelperMysql.Query(strSql).Tables[0];
        }

        public static DataTable GetTableX(int iTableNum, int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            if (iTableNum < 1 || iTableNum > 8)
            {
                return new DataTable();
            }

            string sqlStr = string.Format("CALL GetBaseData({2},{0},{1})", iPageIndex, iPageSize, iTableNum);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return data;
            //DataSet ds = Common.DirectDbHelperMysql.Query(SSDC.Common.CreateBaseDataSql.CreateBaseDataSelectAllSql(iTableNum));

            //if (ds != null)
            //{
            //    return ds.Tables[0];
            //}
            //else
            //{
            //    return new DataTable();
            //}
        }

        public static DataTable GetIdNumberInfo(string iIdNumber, string iName)
        {
            DataTable dtResult = Common.DirectDbHelperMysql.Query(SSDC.Common.CreateBaseDataSql.CreateBaseDataSelectSql(iIdNumber, iName)).Tables[0];

            return dtResult;
        }

        public static void CloneDataTable(DataTable iTo, DataTable iFrom)
        {
            if (iFrom != null && iFrom.Rows.Count > 0)
            {
                foreach (DataRow row in iFrom.Rows)
                {
                    DataRow newRow = iTo.NewRow();
                    for (int j = 0; j < iFrom.Columns.Count; j++)
                    {
                        newRow[j] = row[j];
                    }

                    iTo.Rows.Add(newRow);
                }

            }
        }
    }
}

