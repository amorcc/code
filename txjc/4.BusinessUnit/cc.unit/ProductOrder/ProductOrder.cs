using cc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.ProductOrder
{
    public class ProductOrder
    {
        #region 买家订单列表

        public List<VProductOrderInfo> GetBuyersList(cc.common.UserInfo iLoginUser, string iOrderCode, int iOrderStatus, string iSupplierName, string iStartDate, string iEndDate, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            return poDal.GetBuyersList(iLoginUser, iOrderCode, iOrderStatus, iSupplierName, iStartDate, iEndDate, iPageIndex, iPageSize, out iTotalRows, out iTotalPages);
        }
        #endregion

        #region 卖家订单列表
        public List<VProductOrderInfo> GetSellerList(cc.common.UserInfo iLoginUser, string iOrderCode, int iOrderStatus, string iRetailerName, string iStartDate, string iEndDate, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            return poDal.GetSellerList(iLoginUser, iOrderCode, iOrderStatus, iRetailerName, iStartDate, iEndDate, iPageIndex, iPageSize, out iTotalRows, out iTotalPages);
        }
        #endregion

        #region 买家取消订单
        public bool BuyersOrderCancel(cc.common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            return poDal.BuyersOrderCancel(iLoginUser, iOrderCode, out iErrorMsg);
        }
        #endregion


        #region 卖家取消订单
        public bool SellerOrderCancel(cc.common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            return poDal.SellerOrderCancel(iLoginUser, iOrderCode, out iErrorMsg);
        }
        #endregion

        #region 卖家出库获取订单信息
        public VProductOrderInfo SellerStoreOutBefore(cc.common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            return poDal.SellerStoreOutBefore(iLoginUser, iOrderCode, out iErrorMsg);
        }
        #endregion

        #region 订单出库
        public bool SellerStoreOut(common.UserInfo iLoginUser, string iOrderCode, string iStoreOutInfo, out string iErrorMsg)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            return poDal.SellerStoreOut(iLoginUser, iOrderCode, iStoreOutInfo, out iErrorMsg);
        }
        #endregion

        #region 买家确认收货
        public bool ConfirmReceipt(common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            return poDal.ConfirmReceipt(iLoginUser, iOrderCode, out iErrorMsg);
        }
        #endregion


        #region 订单核销
        public bool OrderWriteOff(common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            return poDal.OrderWriteOff(iLoginUser, iOrderCode, out iErrorMsg);
        }
        #endregion

        #region 获取订单信息
        public VProductOrderInfo GetOrderInfo(common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            return poDal.GetOrderInfo(iLoginUser, iOrderCode, out iErrorMsg);
        }
        #endregion

        #region 订单改价
        public bool OrderChangePrice(common.UserInfo iLoginUser, string iOrderCode, string iNewTransFee, string iNewProPriceInfo, out string iErrorMsg)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            return poDal.OrderChangePrice(iLoginUser, iOrderCode, iNewTransFee, iNewProPriceInfo, out iErrorMsg);
        }
        #endregion
    }
}
