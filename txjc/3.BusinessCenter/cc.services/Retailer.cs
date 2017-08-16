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
    public class Retailer : IRetailer
    {
        common.Utility.ActionResult<JObject> IRetailer.GetMySupplier(common.UserInfo iLoginUser, int iPageIndex, int iPageSize)
        {
            int totalRows = 0;
            int totalPages = 0;
            cc.unit.Retailer.RetailerMng rmBu = new unit.Retailer.RetailerMng();
            List<VMySupplierInfo> mySupplierList = rmBu.GetMySupplier(iLoginUser, iPageIndex, iPageSize, out totalRows, out totalPages);

            JArray lst = new JArray();

            if (mySupplierList != null)
            {
                foreach (var item in mySupplierList)
                {
                    lst.Add(new JObject()
                    {
                        {"BusinessScope",item.BusinessScope},
                        {"CompanyName",item.CompanyName},
                        {"Id",item.Id},
                        {"UserSN_S",item.UserSN_S},
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


        common.Utility.ActionResult<VCompanyInfo> IRetailer.SearchSupplier(common.UserInfo iLoginUser, string iUserSN)
        {
            cc.unit.Retailer.RetailerMng rmBu = new unit.Retailer.RetailerMng();
            var result = rmBu.SearchSupplier(iLoginUser, iUserSN);

            //if (result != null)
            //{
            return common.Utility.MyResponse.ToYou<VCompanyInfo>(result);
            //}
            //else
            //{
            //    return common.Utility.MyResponse.ShowError<VCompanyInfo>("未查询信息，请检查是否输入正确");
            //}
        }


        common.Utility.ActionResult<int> IRetailer.AddSupplier(common.UserInfo iLoginUser, string iUserSN_S, string iUserSN_R)
        {
            string errorMsg = string.Empty;

            cc.unit.Retailer.RetailerMng retailerBU = new unit.Retailer.RetailerMng();
            int result = retailerBU.AddSupplier(iLoginUser, iUserSN_S, iUserSN_R, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return common.Utility.MyResponse.ToYou<int>(result, "设置成功");
            }
            else
            {
                return common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IRetailer.RemoveSupplier(common.UserInfo iLoginUser, int iUserCanBuyId)
        {
            string errorMsg = string.Empty;

            cc.unit.Retailer.RetailerMng retailerBU = new unit.Retailer.RetailerMng();
            int result = retailerBU.RemoveSupplier(iLoginUser, iUserCanBuyId, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return common.Utility.MyResponse.ToYou<int>(result, "设置成功");
            }
            else
            {
                return common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }
    }
}
