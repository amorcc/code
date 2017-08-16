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
    public class Menu : BaseDAL<MenuInfo>
    {
        public Menu()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "Menu";
            this._sortField = "Weight";
            this._isDesc = false;
        }

        protected override MenuInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new MenuInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);


            obj.IsShow = Convert.ToInt32(dr["IsShow"] == DBNull.Value ? 0 : dr["IsShow"]);
            obj.Role = Convert.ToInt32(dr["Role"] == DBNull.Value ? 0 : dr["Role"]);
            obj.Weight = Convert.ToInt32(dr["Weight"] == DBNull.Value ? 0 : dr["Weight"]);
            obj.Url = Convert.ToString(dr["Url"] == DBNull.Value ? "" : dr["Url"]);
            obj.Icon = Convert.ToString(dr["Icon"] == DBNull.Value ? "" : dr["Icon"]);
            obj.MenuName = Convert.ToString(dr["MenuName"] == DBNull.Value ? "" : dr["MenuName"]);
            return obj;
        }

        public List<MenuInfo> GetAll()
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("IsShow", "1", OperateType.Equal);

            return base.List(sc.ConditionStr);

            //1,3,4,7,8,9,13,24,25
        }
    }
}
