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
    public class StoreOutDateil : BaseDAL<VStoreOutDetailInfo>
    {
        public StoreOutDateil()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "v_StoreOut_Detail";
            this._sortField = "Id";
            this._isDesc = false;
        }

        protected override VStoreOutDetailInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new VStoreOutDetailInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.StoreOutCode = Convert.ToString(dr["StoreOutCode"] == DBNull.Value ? "" : dr["StoreOutCode"]);
            obj.ProId = Convert.ToInt32(dr["ProId"] == DBNull.Value ? 0 : dr["ProId"]);
            obj.ProCount = Convert.ToInt32(dr["ProCount"] == DBNull.Value ? 0 : dr["ProCount"]);
            obj.ProPrice = Convert.ToDecimal(dr["ProPrice"] == DBNull.Value ? 0 : dr["ProPrice"]);
            obj.ProPrice1 = Convert.ToDecimal(dr["ProPrice1"] == DBNull.Value ? 0 : dr["ProPrice1"]);

            return obj;
        }

        public bool Insert(common.UserInfo iLoginUser, int iProId, string iStoreOutCode, string iOrderCode, int iStoreOutNum, decimal iPrice, int iOrderProCount, SqlTransaction iTrans, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var sp = "[proc_ProductOrder_StoreOut_Detail_Insert]";
            var paras = new[]
                    {
                        new SqlParameter("@proId", iProId),//订单号
                        new SqlParameter("@storeOutCode", iStoreOutCode),//
                        new SqlParameter("@orderCode", iOrderCode),//
                        new SqlParameter("@proCount", iStoreOutNum),//
                        new SqlParameter("@price", iPrice),//
                        new SqlParameter("@price1", iPrice),//
                        new SqlParameter("@orderProCount", iOrderProCount),//
                        new SqlParameter("@msg",null)
                        {Direction = ParameterDirection.Output,Size = 200,SqlDbType = SqlDbType.VarChar}
                    };
            SqlHelper.ExecuteNonQuery(iTrans, CommandType.StoredProcedure, sp, paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;
        }

        public List<VStoreOutDetailInfo> GetStoreOutDetailList(string iOrderCode)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("OrderCode", iOrderCode, OperateType.Equal);

            return base.List(sc.ConditionStr);
        }
    }
}
