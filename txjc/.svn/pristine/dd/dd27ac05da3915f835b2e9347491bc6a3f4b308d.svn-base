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
    public class Supplier : ISupplier
    {
        common.Utility.ActionResult<JObject> ISupplier.GetMyRetailer(common.UserInfo iLoginUser, int iPageIndex, int iPageSize)
        {
            int totalRows = 0;
            int totalPages = 0;
            cc.unit.Supplier.SupplierMng smBU = new unit.Supplier.SupplierMng();
            List<VMyRetailerInfo> myRetailerList = smBU.GetMyRetailer(iLoginUser, iPageIndex, iPageSize, out totalRows, out totalPages);

            JArray lst = new JArray();

            if (myRetailerList != null)
            {
                foreach (var item in myRetailerList)
                {
                    lst.Add(new JObject()
                    {
                        {"BusinessScope",item.BusinessScope},
                        {"CompanyName",item.CompanyName},
                        {"Id",item.Id},
                        {"UserSN_R",item.UserSN_R},
                        {"DateAdded",item.DateAdded.ToString("yyyy-MM-dd")},
                    });
                }
            }

            JObject result = new JObject()
            {
                {"Data", lst},
                {"TotalRows", totalRows},
                {"TotalPages", totalPages},
            };

            return cc.common.Utility.MyResponse.ToYou<JObject>(result);
        }
    }
}
