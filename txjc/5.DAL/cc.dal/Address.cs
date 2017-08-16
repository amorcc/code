using cc.basedal;
using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.dal
{
    public class Address : BaseDAL<AddressInfo>
    {
        public Address()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "Address";
            this._sortField = "Id";
            this._isDesc = false;
        }

        protected override AddressInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new AddressInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.IsDefault = Convert.ToInt32(dr["IsDefault"] == DBNull.Value ? 0 : dr["IsDefault"]);
            obj.Province = Convert.ToInt32(dr["Province"] == DBNull.Value ? 0 : dr["Province"]);
            obj.City = Convert.ToInt32(dr["City"] == DBNull.Value ? 0 : dr["City"]);
            obj.Zone = Convert.ToInt32(dr["Zone"] == DBNull.Value ? 0 : dr["Zone"]);

            obj.AddressDetail = Convert.ToString(dr["AddressDetail"] == DBNull.Value ? "" : dr["AddressDetail"]);
            obj.AddressTotal = Convert.ToString(dr["AddressTotal"] == DBNull.Value ? "" : dr["AddressTotal"]);
            obj.ZipCode = Convert.ToString(dr["ZipCode"] == DBNull.Value ? "" : dr["ZipCode"]);
            obj.Receiver = Convert.ToString(dr["Receiver"] == DBNull.Value ? "" : dr["Receiver"]);
            obj.Phone = Convert.ToString(dr["Phone"] == DBNull.Value ? "" : dr["Phone"]);
            obj.ReceiverIDCard = Convert.ToString(dr["ReceiverIDCard"] == DBNull.Value ? "" : dr["ReceiverIDCard"]);
            obj.AuditStatus = Convert.ToString(dr["AuditStatus"] == DBNull.Value ? "" : dr["AuditStatus"]);
            obj.AudiFile = Convert.ToString(dr["AudiFile"] == DBNull.Value ? "" : dr["AudiFile"]);
            obj.C1 = Convert.ToString(dr["C1"] == DBNull.Value ? "" : dr["C1"]);
            obj.C2 = Convert.ToString(dr["C2"] == DBNull.Value ? "" : dr["C2"]);
            obj.UserSN = Convert.ToString(dr["UserSN"] == DBNull.Value ? "" : dr["UserSN"]);

            return obj;
        }

        public List<AddressInfo> GetAddressByUserSN(string iUserSN)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN", iUserSN, OperateType.Equal);

            return base.List(sc.ConditionStr);
        }

        public bool Insert(cc.common.UserInfo iLoginUser, int iProvince, int iCity, int iZone, string iReceiver, string iPhone, string iAddressDetail)
        {
            cc.dal.Area areaDal = new Area();
            string pname = areaDal.GetAreaModel(iProvince).Name;
            string cName = areaDal.GetAreaModel(iCity).Name;
            string zoneName = areaDal.GetAreaModel(iZone).Name;

            string AddressTotal = string.Format("{0} - {1} - {2} {3}", pname, cName, zoneName, iAddressDetail);

            int IsDefault = 0;

            List<AddressInfo> oldAddressList = this.GetAddressByUserSN(iLoginUser.UserSN);

            if (oldAddressList == null || oldAddressList.Count == 0)
            {
                IsDefault = 1;
            }

            JObject para = new JObject()
            {
                {"Receiver", iReceiver},
                {"Phone", iPhone},
                {"Province", iProvince},
                {"City", iCity},
                {"Zone", iZone},
                {"AddressDetail", iAddressDetail},
                {"AddressTotal", AddressTotal},
                {"IsDefault", IsDefault},
                {"UserSN", iLoginUser.UserSN},
            };

            return base.Insert(para.ToString());
        }
    }
}
