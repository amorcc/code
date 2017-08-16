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
    public class Cart : BaseDAL<CartInfo>
    {
        public Cart()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "v_Cart";
            this._sortField = "Id";
            this._isDesc = false;
        }

        protected override CartInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new CartInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.ActivityID = Convert.ToInt32(dr["ActivityID"] == DBNull.Value ? 0 : dr["ActivityID"]);
            obj.C1 = Convert.ToString(dr["C1"] == DBNull.Value ? "" : dr["C1"]);
            obj.OldProPrice = Convert.ToDecimal(dr["OldProPrice"] == DBNull.Value ? 0 : dr["OldProPrice"]);
            obj.ProCount = Convert.ToInt32(dr["ProCount"] == DBNull.Value ? 0 : dr["ProCount"]);
            obj.ProId = Convert.ToInt32(dr["ProId"] == DBNull.Value ? 0 : dr["ProId"]);
            obj.ProPrice = Convert.ToDecimal(dr["ProPrice"] == DBNull.Value ? 0 : dr["ProPrice"]);
            obj.Status = Convert.ToInt32(dr["Status"] == DBNull.Value ? 0 : dr["Status"]);
            obj.UserID = Convert.ToInt32(dr["UserID"] == DBNull.Value ? 0 : dr["UserID"]);
            obj.UserSN_R = Convert.ToString(dr["UserSN_R"] == DBNull.Value ? "" : dr["UserSN_R"]);
            obj.UserSN_S = Convert.ToString(dr["UserSN_S"] == DBNull.Value ? "" : dr["UserSN_S"]);
            obj.CompanyName = Convert.ToString(dr["CompanyName"] == DBNull.Value ? "" : dr["CompanyName"]);
            obj.ProName = Convert.ToString(dr["ProName"] == DBNull.Value ? "" : dr["ProName"]);
            obj.ProImage = Convert.ToString(dr["ProImage"] == DBNull.Value ? "" : dr["ProImage"]);
            obj.Amount = Convert.ToInt32(dr["Amount"] == DBNull.Value ? 0 : dr["Amount"]);

            return obj;
        }

        public int AddToCart(int iUserId, string iUserSN_R, int iProId, int iProCount, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var paras = new[]
                    {
                        new SqlParameter("@userSN_R", iUserSN_R),
                        new SqlParameter("@proId", iProId),
                        new SqlParameter("@proCount", iProCount),
                        new SqlParameter("@sysUserId", iUserId),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, "proc_Product_CartAdd", paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return 0;
            }

            return 1;
        }

        public string RemoveCart(string iUserSN_R, List<int> iProId, SqlTransaction trans = null)
        {
            //string proIds = string.Join(",", iProId);
            //SearchCondition sc = new SearchCondition();
            //sc.AddCondition("UserSN_R", iUserSN_R, OperateType.Equal);
            //sc.AddCondition("ProId", proIds, OperateType.In);

            //return base.DeleteByCondition(sc.ConditionStr, trans);
            string iErrorMsg = string.Empty;
            var paras = new[]
                    {
                        new SqlParameter("@userSN", iUserSN_R),
                        new SqlParameter("@proIds", string.Join(",",iProId.ToArray())),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, "proc_Cart_Remove", paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
            }

            return iErrorMsg;

        }

        /// <summary>
        /// 购物车数量
        /// </summary>
        /// <param name="iUserSN_R"></param>
        /// <returns></returns>
        public int GetCartCount(string iUserSN_R)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN_R", iUserSN_R, OperateType.Equal);

            return base.GetCount(sc.ConditionStr);
        }

        /// <summary>
        /// 修改购物车商品数量
        /// </summary>
        /// <param name="iLoginUser"></param>
        /// <param name="iProId"></param>
        /// <param name="iModifyCount">新的修改后的数量</param>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public bool CartModifyCount(cc.common.UserInfo iLoginUser, int iProId, int iModifyCount, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var paras = new[]
                    {
                        new SqlParameter("@userSN_R", iLoginUser.UserSN),
                        new SqlParameter("@proId", iProId),
                        new SqlParameter("@modifyCount", iModifyCount),
                        new SqlParameter("@sysUserId", iLoginUser.UserId),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, "[proc_Cart_ModifyCount]", paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取购物车信息
        /// </summary>
        /// <param name="iUserSN_R"></param>
        /// <returns></returns>
        public JArray GetCartInfo(string iUserSN_R)
        {

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN_R", iUserSN_R, OperateType.Equal);

            List<CartInfo> lst = base.List(sc.ConditionStr);

            List<string> supplier = (from t in lst
                                     select t.UserSN_S).Distinct().ToList();
            JArray supplierJarray = new JArray();

            foreach (string userSN_S in supplier)
            {
                string companyName = string.Empty;
                JArray proJarray = new JArray();
                #region 查找产品信息
                List<CartInfo> supplierProList = (from t in lst
                                                  where t.UserSN_S == userSN_S
                                                  select t).ToList();

                if (supplierProList != null && supplierProList.Count > 0)
                {
                    companyName = supplierProList.FirstOrDefault().CompanyName;

                    foreach (var item in supplierProList)
                    {
                        proJarray.Add(new JObject()
                        {
                            {"Id", item.Id},
                            {"ProCount", item.ProCount},
                            {"ProId", item.ProId},
                            {"ProPrice", item.ProPrice.ToString("0.00")},
                            {"Status", item.Status},
                            {"UserSN_R", item.UserSN_R},
                            {"UserSN_S", item.UserSN_S},
                            {"ProName", item.ProName},
                            {"ProImage", item.ProImage},
                            {"Amount", item.Amount},
                        });
                    }
                }
                #endregion

                if (proJarray.Count > 0)
                {
                    supplierJarray.Add(new JObject()
                    {
                        {"CompanyName",companyName},
                        {"UserSN_S",userSN_S},
                        {"ProList",proJarray},
                    });
                }
            }

            return supplierJarray;
        }

    }
}
