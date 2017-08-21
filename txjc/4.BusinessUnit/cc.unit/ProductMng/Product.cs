using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.ProductMng
{

    public class Product
    {
        public int AddToCart(int iUserId, string iUserSN_R, int iProId, int iProCount, out string iErrorMsg)
        {
            cc.dal.Cart cartDal = new dal.Cart();
            return cartDal.AddToCart(iUserId, iUserSN_R, iProId, iProCount, out iErrorMsg);
        }

        public List<VProductInfo> GetRetailerCanBuyProductList(string iUserSN_R, string iKey, string iUserSN_S, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages, out string iErrorMsg)
        {
            iTotalPages = 0;
            iTotalRows = 0;
            cc.dal.Product pDal = new dal.Product();

            return pDal.GetProductList(iUserSN_R, iKey, iUserSN_S, iPageIndex, iPageSize, out iTotalRows, out iTotalPages, out iErrorMsg);

        }

        public List<VProductInfo> GetRetailerCanBuyProductList2(string iUserSN_R, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages)
        {
            iTotalPages = 0;
            iTotalRows = 0;
            cc.dal.UserCanBuy ucbDal = new dal.UserCanBuy();
            List<UserCanBuyInfo> ucbiList = ucbDal.GetRetailerCanBuy(iUserSN_R);

            if (ucbiList != null && ucbiList.Count > 0)
            {
                var supplierSNList = from t in ucbiList
                                     select t.UserSN_S;
                string supplierSNs = string.Join(",", supplierSNList);

                cc.dal.Product productDal = new dal.Product();
                List<VProductInfo> result = productDal.GetProductListByManyUserSN(supplierSNs);

                if (result != null && result.Count > 0)
                {
                    iTotalRows = result.Count();
                    iTotalPages = (iTotalRows / iPageSize) + (iTotalRows % iPageSize > 0 ? 1 : 0);
                    return result.Skip((iPageIndex - 1) * iPageSize).Take(iPageSize).ToList();
                }

                return result;
            }
            else
            {
                return new List<VProductInfo>();
            }

        }

        public VProductInfo GetProductInfo(int iProId)
        {
            cc.dal.Product proDal = new dal.Product();
            return proDal.GetProductInfo(iProId);
        }

        public bool IsCollect(common.UserInfo iLoginUser, int iProId)
        {
            cc.dal.ProductCollect pcDal = new dal.ProductCollect();
            return pcDal.IsCollect(iLoginUser, iProId);
        }

        public List<VProductInfo> SellerProductMng(common.UserInfo iLoginUser, int iStatus, string iProName, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages, out string iErrorMsg)
        {
            cc.dal.Product pDal = new dal.Product();
            return pDal.SellerProductMng(iLoginUser, iStatus, iProName, iPageIndex, iPageSize, out iTotalRows, out iTotalPages, out iErrorMsg);
        }

        public bool UpdateStatus(common.UserInfo iLoginUser, int iProId, int iStatus, out string iErrorMsg)
        {
            cc.dal.Product pDal = new dal.Product();
            return pDal.UpdateStatus(iLoginUser, iProId, iStatus, out iErrorMsg);
        }

        public bool ModifyProduct(common.UserInfo iLoginUser, int iProId, string iProName, int iBillNeeded, int iStatus, int iAmount, decimal iPrice, string iImages, string iDesc, out string iErrorMsg)
        {
            if (string.IsNullOrEmpty(iImages))
            {
                iErrorMsg = "请最低上传一个商品图片";
                return false;
            }

            List<string> images = iImages.Split(',').ToList();

            cc.dal.Product pDal = new dal.Product();
            return pDal.ModifyProduct(iLoginUser, iProId, iProName, iBillNeeded, iStatus, iAmount, iPrice, images, iDesc, out iErrorMsg);
        }

        public bool DeleteProduct(common.UserInfo iLoginUser, int iProId, out string iErrorMsg)
        {
            cc.dal.Product pDal = new dal.Product();
            return pDal.DeleteProduct(iLoginUser, iProId, out iErrorMsg);
        }

        public bool UpdateAmount(common.UserInfo iLoginUser, int iProId, int iAmount, out string iErrorMsg)
        {
            cc.dal.Product pDal = new dal.Product();
            return pDal.UpdateAmount(iLoginUser, iProId, iAmount, out iErrorMsg);
        }

        public bool UpdatePrice(common.UserInfo iLoginUser, int iProId, decimal iPrice, out string iErrorMsg)
        {
            cc.dal.Product pDal = new dal.Product();
            return pDal.UpdatePrice(iLoginUser, iProId, iPrice, out iErrorMsg);
        }

        public JObject GetMyCollectProductInfo(cc.common.UserInfo iLoginUser, int iPageIndex, int iPageSize, out string iErrorMsg)
        {
            cc.dal.ProductCollect pcDal = new dal.ProductCollect();
            return pcDal.GetMyCollectProductInfo(iLoginUser, iPageIndex, iPageSize, out iErrorMsg);

        }

        /// <summary>
        /// 切换商品收藏状态
        /// </summary>
        /// <param name="iLoginUser"></param>
        /// <param name="iProId"></param>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public bool ProductCollectSwitch(common.UserInfo iLoginUser, int iProId, out string iErrorMsg)
        {
            cc.dal.ProductCollect pcDal = new dal.ProductCollect();
            return pcDal.ProductCollectSwitch(iLoginUser, iProId, out iErrorMsg);
        }
    }
}
