using cc.iservices;
using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.services
{
    public class AreaMng : IAreaMng
    {
        common.Utility.ActionResult<Newtonsoft.Json.Linq.JArray> IAreaMng.GetAreaInfo(int iPCode, int iLevel, int iCode)
        {
            cc.unit.Misc.AreaMng areaBU = new unit.Misc.AreaMng();
            List<AreaInfo> lst = areaBU.GetAreaInfo(iPCode, iLevel, iCode);

            JArray result = new JArray();

            foreach (var item in lst)
            {
                result.Add(new JObject()
                {
                    {"Id",item.Id},
                    {"Code",item.Code},
                    {"PCode",item.PCode},
                    {"Name",item.Name},
                    {"FullName",item.FullName},
                    {"Level",item.Level},
                });
            }

            return cc.common.Utility.MyResponse.ToYou<JArray>(result);
        }
    }
}
