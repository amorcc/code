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
    public class ProductMng : IProductMng
    {

        common.Utility.ActionResult<JObject> IProductMng.GetRetailerCanBuyProductList(common.UserInfo iLoginUser, string iKey, int iPageIndex, int iPageSize)
        {
            int totalPages = 0;
            int totalRows = 0;
            string errorMsg = string.Empty;
            cc.unit.ProductMng.Product pBu = new unit.ProductMng.Product();

            List<VProductInfo> lst = pBu.GetRetailerCanBuyProductList(iLoginUser.UserSN, iKey, iPageIndex, iPageSize, out totalRows, out totalPages, out errorMsg);

            JArray lstJarray = new JArray();

            if (string.IsNullOrEmpty(errorMsg))
            {
                if (lst != null && lst.Count > 0)
                {
                    foreach (VProductInfo pro in lst)
                    {
                        lstJarray.Add(new JObject()
                        {
                            {"ProId" , pro.Id},
                            {"ProName" , pro.Name},
                            {"Amount" , pro.Amount},
                            {"Price" , pro.Price.ToString("0.00")},
                            {"BillNeeded" , pro.BillNeeded},
                            {"UserSN_S" , pro.UserSN},
                            {"Image" , pro.Image},
                            {"Supplier" , pro.Supplier},
                        });
                    }
                }

                JObject result = new JObject()
                {
                    {"Data",lstJarray},
                    {"TotalRow",totalRows},
                    {"TotalPages",totalPages},
                };

                return cc.common.Utility.MyResponse.ToYou<JObject>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JObject>(errorMsg);
            }

        }

        /// <summary>
        /// 卖家获取产品列表
        /// </summary>
        /// <param name="iLoginUser"></param>
        /// <returns></returns>
        common.Utility.ActionResult<JObject> IProductMng.GetRetailerCanBuyProductList2(common.UserInfo iLoginUser, int iPageIndex, int iPageSize)
        {
            int totalPages = 0;
            int totalRows = 0;
            cc.unit.ProductMng.Product pBu = new unit.ProductMng.Product();

            List<VProductInfo> lst = pBu.GetRetailerCanBuyProductList2(iLoginUser.UserSN, iPageIndex, iPageSize, out totalRows, out totalPages);

            JArray lstJarray = new JArray();

            if (lst != null && lst.Count > 0)
            {
                foreach (VProductInfo pro in lst)
                {
                    lstJarray.Add(new JObject()
                    {
                        {"ProId" , pro.Id},
                        {"ProName" , pro.Name},
                        {"Amount" , pro.Amount},
                        {"Price" , pro.Price.ToString("0.00")},
                        {"BillNeeded" , pro.BillNeeded},
                        {"UserSN_S" , pro.UserSN},
                        {"Image" , pro.Image},
                    });
                }
            }

            JObject result = new JObject()
            {
                {"Data",lstJarray},
                {"TotalRow",totalRows},
                {"TotalPages",totalPages},
            };

            return cc.common.Utility.MyResponse.ToYou<JObject>(result);
        }

        common.Utility.ActionResult<int> IProductMng.AddToCart(int iUserId, string iUserSN_R, int iProId, int iProCount)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductMng.Product pBU = new unit.ProductMng.Product();
            int resule = pBU.AddToCart(iUserId, iUserSN_R, iProId, iProCount, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(resule, "成功加入购物车!");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<JObject> IProductMng.GetProductInfo(common.UserInfo iLoginUser, int iProId)
        {
            cc.unit.ProductMng.Product proBU = new unit.ProductMng.Product();
            VProductInfo pro = proBU.GetProductInfo(iProId);

            if (pro != null)
            {
                JObject result = new JObject()
                {
                    {"ProId",pro.Id},
                    {"ProName",pro.Name},
                    {"ProImage",cc.utility.Common.App("ApiSiteUrl")+pro.Image},
                    {"ProImages", pro.Images},
                    {"ProPrice",pro.Price.ToString("0.00")},
                    {"ProAmount",pro.Amount},
                    {"Status",pro.Status},
                    {"Desc",pro.Desc},
                    {"Supplier",pro.Supplier},
                    {"UserSN",pro.UserSN},
                    {"BillNeeded",pro.BillNeeded},
                };

                return cc.common.Utility.MyResponse.ToYou<JObject>(result);
            }

            JObject result1 = new JObject();
            return cc.common.Utility.MyResponse.ToYou<JObject>(result1);
        }


        common.Utility.ActionResult<JObject> IProductMng.SellerProductMng(common.UserInfo iLoginUser, int iStatus, string iProName, int iPageIndex, int iPageSize)
        {
            string errorMsg = string.Empty;
            int totalRows = 0;
            int totalPages = 1;

            cc.unit.ProductMng.Product pBU = new unit.ProductMng.Product();
            var proList = pBU.SellerProductMng(iLoginUser, iStatus, iProName, iPageIndex, iPageSize, out totalRows, out totalPages, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg) && proList != null)
            {
                JArray proJA = new JArray();

                foreach (var item in proList)
                {
                    proJA.Add(new JObject()
                    {
                        {"ProName", item.Name},
                        {"ProId", item.Id},
                        {"Amount", item.Amount},
                        {"Status", item.Status},
                        {"ProImage",cc.utility.Common.App("ApiSiteUrl")+ item.Image},
                        {"ProPrice", item.Price.ToString("0.00")},
                    });
                }

                JObject result = new JObject()
                {
                    {"Data",proJA},
                    {"TotalRows",totalRows},
                    {"TotalPages",totalPages},
                };
                return common.Utility.MyResponse.ToYou<JObject>(result);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JObject>(string.IsNullOrEmpty(errorMsg) ? "获取产品信息出错" : errorMsg);
            }
        }


        common.Utility.ActionResult<int> IProductMng.UpdateStatus(common.UserInfo iLoginUser, int iProId, int iStatus)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductMng.Product pBU = new unit.ProductMng.Product();
            int resule = pBU.UpdateStatus(iLoginUser, iProId, iStatus, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(resule, "修改成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IProductMng.ModifyProduct(common.UserInfo iLoginUser, int iProId, string iProName, int iBillNeeded, int iStatus, int iAmount, decimal iPrice, string iImages, string iDesc)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductMng.Product pBU = new unit.ProductMng.Product();
            int resule = pBU.ModifyProduct(iLoginUser, iProId, iProName, iBillNeeded, iStatus, iAmount, iPrice, iImages, iDesc, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(resule, "操作成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IProductMng.DeleteProduct(common.UserInfo iLoginUser, int iProId)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductMng.Product pBU = new unit.ProductMng.Product();
            int resule = pBU.DeleteProduct(iLoginUser, iProId, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(resule, "操作成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }





        common.Utility.ActionResult<int> IProductMng.UpdateAmount(common.UserInfo iLoginUser, int iProId, int iAmount)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductMng.Product pBU = new unit.ProductMng.Product();
            int resule = pBU.UpdateAmount(iLoginUser, iProId, iAmount, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(resule, "操作成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }

        common.Utility.ActionResult<int> IProductMng.UpdatePrice(common.UserInfo iLoginUser, int iProId, decimal iPrice)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductMng.Product pBU = new unit.ProductMng.Product();
            int resule = pBU.UpdatePrice(iLoginUser, iProId, iPrice, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(resule, "操作成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }


        common.Utility.ActionResult<JObject> IProductMng.GetMyCollectProductInfo(common.UserInfo iLoginUser, int iPageIndex, int iPageSize)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductMng.Product pBU = new unit.ProductMng.Product();
            var resule = pBU.GetMyCollectProductInfo(iLoginUser, iPageIndex, iPageSize, out errorMsg);

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<JObject>(resule, "");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<JObject>(errorMsg);
            }
        }


        common.Utility.ActionResult<int> IProductMng.IsCollect(common.UserInfo iLoginUser, int iProId)
        {
            cc.unit.ProductMng.Product pBU = new unit.ProductMng.Product();
            var result = pBU.IsCollect(iLoginUser, iProId) ? 1 : 0;

            return cc.common.Utility.MyResponse.ToYou<int>(result);
        }


        common.Utility.ActionResult<int> IProductMng.ProductCollectSwitch(common.UserInfo iLoginUser, int iProId)
        {
            string errorMsg = string.Empty;

            cc.unit.ProductMng.Product pBU = new unit.ProductMng.Product();
            var resule = pBU.ProductCollectSwitch(iLoginUser, iProId, out errorMsg) ? 1 : 0;

            if (string.IsNullOrEmpty(errorMsg))
            {
                return cc.common.Utility.MyResponse.ToYou<int>(resule, "操作成功");
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>(errorMsg);
            }
        }
    }
}
