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
    public class UserCanBuy : BaseDAL<UserCanBuyInfo>
    {
        public UserCanBuy()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "UserCanBuy";
        }

        protected override UserCanBuyInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new UserCanBuyInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.UserSN_R = Convert.ToString(dr["UserSN_R"] == DBNull.Value ? "" : dr["UserSN_R"]);
            obj.UserSN_S = Convert.ToString(dr["UserSN_S"] == DBNull.Value ? "" : dr["UserSN_S"]);

            return obj;
        }

        public List<UserCanBuyInfo> GetRetailerCanBuy(string iUserSN_R)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN_R", iUserSN_R, OperateType.Equal);

            return base.List(sc.ConditionStr);
        }
    }
}
