using cc.basedal;
using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.dal
{
    /// <summary>
    /// 商品收藏
    /// </summary>
    public class ProductCollect : BaseDAL<ProductCollectInfo>
    {
        public ProductCollect()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "ProductCollect";
            this._isDesc = true;
        }

        protected override ProductCollectInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new ProductCollectInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.ProId = Convert.ToInt32(dr["ProId"] == DBNull.Value ? 0 : dr["ProId"]);
            obj.UserSN = Convert.ToString(dr["UserSN"] == DBNull.Value ? "" : dr["UserSN"]);

            return obj;
        }

        public JObject GetMyCollectProductInfo(cc.common.UserInfo iLoginUser, int iPageIndex, int iPageSize, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            int iTotalPages = 1;
            int iTotalRows = 0;
            var paras = new[]
                    {
                        new SqlParameter("@userSN", iLoginUser.UserSN),
                        new SqlParameter("@PageIndex", iPageIndex),
                        new SqlParameter("@PageSize", iPageSize),
                        new SqlParameter(){ParameterName = "@totalPages",SqlDbType = SqlDbType.Int,Size = 4,Direction = ParameterDirection.Output},
                        new SqlParameter(){ParameterName = "@totalRows",SqlDbType = SqlDbType.Int,Size = 4,Direction = ParameterDirection.Output},
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            using (DataSet ds = SqlHelper.ExecuteDataset(this._sqlCon, CommandType.StoredProcedure, "proc_Product_MyCollect", paras))
            {
                var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
                if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                {
                    iErrorMsg = msg.ToString();
                    return null;
                }

                iTotalRows = Convert.ToInt32(paras[paras.Length - 2].Value);
                iTotalPages = Convert.ToInt32(paras[paras.Length - 3].Value);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    JArray lst = new JArray();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lst.Add(new JObject()
                        {
                            {"Id",Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"])},
                            {"ProId",Convert.ToInt32(dr["ProId"] == DBNull.Value ? 0 : dr["ProId"])},
                            {"DateAdded",Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"])},
                            {"Amount",Convert.ToInt32(dr["Amount"] == DBNull.Value ? 0 : dr["Amount"])},
                            {"BillNeeded",Convert.ToInt32(dr["BillNeeded"] == DBNull.Value ? 0 : dr["BillNeeded"])},
                            {"Desc",Convert.ToString(dr["Desc"] == DBNull.Value ? "" : dr["Desc"])},
                            {"Image",Convert.ToString(dr["Image"] == DBNull.Value ? "" : dr["Image"])},
                            {"Images",Convert.ToString(dr["Images"] == DBNull.Value ? "" : dr["Images"])},
                            {"ProName",Convert.ToString(dr["Name"] == DBNull.Value ? "" : dr["Name"])},
                            {"Price",Convert.ToDecimal(dr["Price"] == DBNull.Value ? 0 : dr["Price"])},
                            {"SaledRecent30Days",Convert.ToInt32(dr["SaledRecent30Days"] == DBNull.Value ? 0 : dr["SaledRecent30Days"])},
                            {"Status",Convert.ToInt32(dr["Status"] == DBNull.Value ? 0 : dr["Status"])},
                            {"UserSN",Convert.ToString(dr["UserSN"] == DBNull.Value ? "" : dr["UserSN"])},
                            {"Supplier",Convert.ToString(dr["Supplier"] == DBNull.Value ? "" : dr["Supplier"])},
                        });
                    }

                    JObject result = new JObject()
                        {
                           {"TotalRows}",iTotalRows},
                           {"TotalPages",iTotalPages},
                           {"Data",lst},
                        };

                    return result;
                }
            }

            return null;
        }

        public bool IsCollect(common.UserInfo iLoginUser, int iProId)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN", iLoginUser.UserSN, OperateType.Equal);
            sc.AddCondition("ProId", iProId.ToString(), OperateType.Equal);

            if (base.GetCount(sc.ConditionStr) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ProductCollectSwitch(common.UserInfo iLoginUser, int iProId, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            try
            {
                var sp = "[proc_Product_CollectSwitch]";
                var paras = new[]
                    {
                        new SqlParameter("@proId", iProId),
                        new SqlParameter("@userSN", iLoginUser.UserSN),
                        new SqlParameter("@sysUserID", iLoginUser.UserId), //权限控制 及参数列表
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}
                    };
                SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, sp, paras);
                var msg = paras[paras.Length - 1].Value;
                if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                {
                    iErrorMsg = msg.ToString();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                iErrorMsg = ex.ToString();
                return false;
            }
        }
    }
}
