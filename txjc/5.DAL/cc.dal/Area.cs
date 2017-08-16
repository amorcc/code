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
    public class Area : BaseDAL<AreaInfo>
    {
        public Area()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "Area";
            this._sortField = "Id";
            this._isDesc = false;
        }

        protected override AreaInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new AreaInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.InUse = Convert.ToInt32(dr["InUse"] == DBNull.Value ? 0 : dr["InUse"]);
            obj.Code = Convert.ToInt32(dr["Code"] == DBNull.Value ? 0 : dr["Code"]);
            obj.PCode = Convert.ToInt32(dr["PCode"] == DBNull.Value ? 0 : dr["PCode"]);
            obj.Level = Convert.ToInt32(dr["Level"] == DBNull.Value ? 0 : dr["Level"]);

            obj.Name = Convert.ToString(dr["Name"] == DBNull.Value ? "" : dr["Name"]);
            obj.FullName = Convert.ToString(dr["FullName"] == DBNull.Value ? "" : dr["FullName"]);

            return obj;
        }

        public List<AreaInfo> GetAllArea()
        {
            List<AreaInfo> cache = cc.common.Cache.CacheMng<AreaInfo>.GetCache(this._tableName);

            if (cache == null || cache.Count == 0)
            {

                SearchCondition sc = new SearchCondition();
                sc.AddCondition("InUse", "1", OperateType.Equal);

                List<AreaInfo> result = base.List(sc.ConditionStr);

                foreach (var item in result)
                {
                    item._CacheTimeOutSeconds = -1;
                }

                if (cache != null)
                {
                    cache.Clear();
                }

                cc.common.Cache.CacheMng<AreaInfo>.AddCache(this._tableName, result);

            }

            return cache;
        }

        public List<AreaInfo> GetAreaInfo(int iPCode, int iLevel, int iCode)
        {
            List<AreaInfo> allArea = this.GetAllArea();

            if (iPCode > -1 && iLevel == -1 && iCode == -1)
            {
                //A
                return (from t in allArea
                        where t.PCode == iPCode
                        select t).ToList();
            }
            else if (iPCode == -1 && iLevel > -1 && iCode == -1)
            {
                //B
                return (from t in allArea
                        where t.Level == iLevel
                        select t).ToList();
            }
            else if (iPCode == -1 && iLevel == -1 && iCode > -1)
            {
                //C
                return (from t in allArea
                        where t.Code == iCode
                        select t).ToList();
            }
            else if (iPCode > -1 && iLevel > -1 && iCode == -1)
            {
                //AB
                return (from t in allArea
                        where t.PCode == iPCode && t.Level == iLevel
                        select t).ToList();
            }
            else if (iPCode > -1 && iLevel == -1 && iCode > -1)
            {
                //AC
                return (from t in allArea
                        where t.PCode == iPCode && t.Code == iCode
                        select t).ToList();
            }
            else if (iPCode == -1 && iLevel > -1 && iCode > -1)
            {
                //BC
                return (from t in allArea
                        where t.Code == iCode && t.Level == iLevel
                        select t).ToList();
            }
            else if (iPCode > -1 && iLevel > -1 && iCode > -1)
            {
                //ABC
                return (from t in allArea
                        where t.Code == iCode && t.PCode == iPCode && t.Level == iLevel
                        select t).ToList();
            }
            else
            {
                return allArea;
            }
        }

        public AreaInfo GetAreaModel(int iCode)
        {
            List<AreaInfo> allArea = this.GetAllArea();
            AreaInfo find = (from t in allArea
                             where t.Code == iCode
                             select t).FirstOrDefault();

            if (find == null)
            {
                return new AreaInfo();
            }

            return find;
        }
    }
}
