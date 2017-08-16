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
    public class StoreOut : BaseDAL<VStoreOutInfo>
    {
        public StoreOut()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "v_StoreOut";
            this._sortField = "Id";
            this._isDesc = false;
        }

        protected override VStoreOutInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new VStoreOutInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.OrderCode = Convert.ToString(dr["OrderCode"] == DBNull.Value ? "" : dr["OrderCode"]);
            obj.Express = Convert.ToString(dr["Express"] == DBNull.Value ? "" : dr["Express"]);
            obj.ExpDesc = Convert.ToString(dr["ExpDesc"] == DBNull.Value ? "" : dr["ExpDesc"]);
            obj.UserSN_S = Convert.ToString(dr["UserSN_S"] == DBNull.Value ? "" : dr["UserSN_S"]);
            obj.UserSN_R = Convert.ToString(dr["UserSN_R"] == DBNull.Value ? "" : dr["UserSN_R"]);
            obj.StoreCode = Convert.ToString(dr["StoreCode"] == DBNull.Value ? "" : dr["StoreCode"]);

            obj.ExpId = Convert.ToInt32(dr["ExpId"] == DBNull.Value ? 0 : dr["ExpId"]);
            obj.ExpCode = Convert.ToString(dr["ExpCode"] == DBNull.Value ? "" : dr["ExpCode"]);
            obj.StoreOutNum = Convert.ToInt32(dr["StoreOutNum"] == DBNull.Value ? 0 : dr["StoreOutNum"]);
            obj.ExpressId = Convert.ToInt32(dr["ExpressId"] == DBNull.Value ? 0 : dr["ExpressId"]);
            obj.SmsRemindTimes = Convert.ToInt32(dr["SmsRemindTimes"] == DBNull.Value ? 0 : dr["SmsRemindTimes"]);
            obj.Status = Convert.ToInt32(dr["Status"] == DBNull.Value ? 0 : dr["Status"]);

            obj.ConfirmDateTime = Convert.ToDateTime(dr["ConfirmDateTime"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["ConfirmDateTime"]);
            obj.ExpDateTime = Convert.ToDateTime(dr["ExpDateTime"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["ExpDateTime"]);

            return obj;
        }

        public bool Insert(common.UserInfo iLoginUser, string iOrdercode, string iStoreOutCode, int iProCount, string iUserSN_R, SqlTransaction iTrans, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var sp = "[proc_ProductOrder_StoreOut_Insert]";
            var paras = new[]
                    {
                        new SqlParameter("@storeCode", iStoreOutCode),//订单号
                        new SqlParameter("@proCount", iProCount),//
                        new SqlParameter("@orderCode", iOrdercode),//
                        new SqlParameter("@userSN_S", iLoginUser.UserSN),//
                        new SqlParameter("@userSN_R", iUserSN_R),//
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

        public List<VStoreOutInfo> GetStoreOutInfo(string iOrderCode, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            List<VStoreOutInfo> resule = null;

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("OrderCode", iOrderCode, OperateType.Equal);
            resule = base.List(sc.ConditionStr);

            if (resule == null || resule.Count == 0)
            {
                iErrorMsg = "没有查询到该订单的出库信息";
                return null;
            }

            cc.dal.StoreOutDateil sodDal = new StoreOutDateil();
            List<VStoreOutDetailInfo> detail = sodDal.GetStoreOutDetailList(iOrderCode);

            if (detail == null || detail.Count == 0)
            {
                iErrorMsg = "没有查询到出库详情信息";
                return null;
            }

            foreach (var item in resule)
            {
                List<VStoreOutDetailInfo> thisDetail = (from t in detail
                                                        where t.StoreOutCode == item.StoreCode
                                                        select t).ToList();

                item.DetailList = thisDetail;
            }

            return resule;
        }

        public bool SellerDeliverGoodsInfo(common.UserInfo iLoginUser, string iStoreOutCode, int iExpId, string iExpNum, out DataSet ds, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var sp = "proc_ProductOrder_DeliverGoods";
            var paras = new[]
                    {
                        new SqlParameter("@storeOutCode", iStoreOutCode),//订单号
                        new SqlParameter("@expId", iExpId),//
                        new SqlParameter("@expNum", iExpNum),//
                        new SqlParameter("@msg",null)
                        {Direction = ParameterDirection.Output,Size = 200,SqlDbType = SqlDbType.VarChar}
                    };
            ds = SqlHelper.ExecuteDataset(cc.common.Sys.SystemConnections.B2bConn, CommandType.StoredProcedure, sp, paras);
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
