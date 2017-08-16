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
    public class MyRetailer : BaseDAL<VMyRetailerInfo>
    {
        public MyRetailer()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "v_MyRetailer";
            this._sortField = "DateAdded";
            this._isDesc = true;
        }

        protected override VMyRetailerInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new VMyRetailerInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);


            obj.UserSN_R = Convert.ToString(dr["UserSN_R"] == DBNull.Value ? "" : dr["UserSN_R"]);
            obj.CompanyName = Convert.ToString(dr["CompanyName"] == DBNull.Value ? "" : dr["CompanyName"]);
            obj.BusinessScope = Convert.ToString(dr["BusinessScope"] == DBNull.Value ? "" : dr["BusinessScope"]);
            return obj;
        }

        public List<VMyRetailerInfo> GetMyRetailer(common.UserInfo iLoginUser, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN_S", iLoginUser.UserSN, OperateType.Equal);

            //return base.List(sc.ConditionStr);
            PageInfo pi = new PageInfo();
            pi.PageIndex = iPageIndex;
            pi.PageSize = iPageSize;

            var result = base.List(sc.ConditionStr, pi);

            iTotalRows = pi.TotalRows;
            iTotalPages = pi.TotalPages;

            return result;
        }
    }
}
