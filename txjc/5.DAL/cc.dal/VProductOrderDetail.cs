using cc.basedal;
using cc.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.dal
{
    public class VProductOrderDetail : BaseDAL<VProductOrderDetailInfo>
    {
        public VProductOrderDetail()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "V_ProductOrder_Detail";
            this._sortField = "Id";
            this._isDesc = false;
        }

        protected override VProductOrderDetailInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new VProductOrderDetailInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.OrderCode = Convert.ToString(dr["OrderCode"] == DBNull.Value ? "" : dr["OrderCode"]);
            obj.SubOrderCode = Convert.ToString(dr["SubOrderCode"] == DBNull.Value ? "" : dr["SubOrderCode"]);
            obj.ProName = Convert.ToString(dr["ProName"] == DBNull.Value ? "" : dr["ProName"]);
            obj.ProImage = Convert.ToString(dr["ProImage"] == DBNull.Value ? "" : dr["ProImage"]);
            obj.ProCount = Convert.ToInt32(dr["ProCount"] == DBNull.Value ? 0 : dr["ProCount"]);
            obj.ProId = Convert.ToInt32(dr["ProId"] == DBNull.Value ? 0 : dr["ProId"]);
            obj.SendCount = Convert.ToInt32(dr["SendCount"] == DBNull.Value ? 0 : dr["SendCount"]);
            obj.ReturnCount = Convert.ToInt32(dr["ReturnCount"] == DBNull.Value ? 0 : dr["ReturnCount"]);
            obj.StoreOutNum = Convert.ToInt32(dr["StoreOutNum"] == DBNull.Value ? 0 : dr["StoreOutNum"]);
            obj.SubTotal = Convert.ToDecimal(dr["SubTotal"] == DBNull.Value ? 0 : dr["SubTotal"]);
            obj.TransFee = Convert.ToDecimal(dr["TransFee"] == DBNull.Value ? 0 : dr["TransFee"]);
            obj.ProPrice = Convert.ToDecimal(dr["ProPrice"] == DBNull.Value ? 0 : dr["ProPrice"]);

            return obj;
        }

        public List<VProductOrderDetailInfo> GetOrderDetailList(string iOrderCodes)
        {
            if (string.IsNullOrEmpty(iOrderCodes))
            {
                return new List<VProductOrderDetailInfo>();
            }

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("OrderCode", iOrderCodes, OperateType.In);
            return base.List(sc.ConditionStr);
        }

        public List<VProductOrderDetailInfo> GetOrderDetailByOrderCode(string iOrderCode)
        {
            if (string.IsNullOrEmpty(iOrderCode))
            {
                return new List<VProductOrderDetailInfo>();
            }

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("OrderCode", iOrderCode, OperateType.Equal);
            return base.List(sc.ConditionStr);
        }
    }
}
