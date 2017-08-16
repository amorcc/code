using cc.basedal;
using cc.model;
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
    /// 买家我的供货商
    /// </summary>
    public class MySupplier : BaseDAL<VMySupplierInfo>
    {
        public MySupplier()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "v_MySupplier";
            this._sortField = "DateAdded";
            this._isDesc = true;
        }

        protected override VMySupplierInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new VMySupplierInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);


            obj.UserSN_S = Convert.ToString(dr["UserSN_S"] == DBNull.Value ? "" : dr["UserSN_S"]);
            obj.CompanyName = Convert.ToString(dr["CompanyName"] == DBNull.Value ? "" : dr["CompanyName"]);
            obj.BusinessScope = Convert.ToString(dr["BusinessScope"] == DBNull.Value ? "" : dr["BusinessScope"]);
            return obj;
        }

        public List<VMySupplierInfo> GetMySupplier(common.UserInfo iLoginUser, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN_R", iLoginUser.UserSN, OperateType.Equal);

            //return base.List(sc.ConditionStr);
            PageInfo pi = new PageInfo();
            pi.PageIndex = iPageIndex;
            pi.PageSize = iPageSize;

            var result = base.List(sc.ConditionStr, pi);

            iTotalRows = pi.TotalRows;
            iTotalPages = pi.TotalPages;

            return result;
        }

        public bool AddSupplier(common.UserInfo iLoginUser, string iUserSN_S, string iUserSN_R, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var paras = new[]
                    {
                        new SqlParameter("@userSN_R", iUserSN_R),
                        new SqlParameter("@userSN_S", iUserSN_S),
                        new SqlParameter("@sysUserId", iLoginUser.UserId),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, "[proc_Retailer_AddSupplier]", paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;
        }

        public bool RemoveSupplier(common.UserInfo iLoginUser, int iUserCanBuyId, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var paras = new[]
                    {
                        new SqlParameter("@canBuyId", iUserCanBuyId),
                        new SqlParameter("@sysUserId", iLoginUser.UserId),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, "[proc_Retailer_RemoveSupplier]", paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;
        }
    }
}
