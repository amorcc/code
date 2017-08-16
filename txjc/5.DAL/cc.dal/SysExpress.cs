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
    public class SysExpress : BaseDAL<SysExpressInfo>
    {
        public SysExpress()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "Sys_Express";
            this._sortField = "OrderNum";
            this._isDesc = false;
        }

        protected override SysExpressInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new SysExpressInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);


            obj.ExpressName = Convert.ToString(dr["ExpressName"] == DBNull.Value ? "" : dr["ExpressName"]);
            obj.ExpressDesc = Convert.ToString(dr["ExpressDesc"] == DBNull.Value ? "" : dr["ExpressDesc"]);
            obj.OrderNum = Convert.ToInt32(dr["OrderNum"] == DBNull.Value ? 0 : dr["OrderNum"]);

            return obj;
        }

        public List<SysExpressInfo> GetAllExpressInfo()
        {
            return base.List();
        }

    }
}
