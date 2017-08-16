using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.common.Utility;
using System.Data;

namespace cc.unit.ProductOrder.DeliverGoods
{
    public class DeliverGoods
    {
        public common.UserInfo mLoginUser;
        public string mOrderCode;
        public VProductOrderInfo mOrderInfo;
        public List<VStoreOutInfo> mStoreOutList;
        cc.dal.ProductOrder mPoDal = new dal.ProductOrder();
        cc.dal.StoreOut mSoDal = new dal.StoreOut();

        public DeliverGoods(common.UserInfo iLoginUser, string iOrderCode)
        {
            this.mLoginUser = iLoginUser;
            this.mOrderCode = iOrderCode;
        }

        #region 获取发货信息
        public JObject GetDeliverGoodsInfo(out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            JObject result = null;

            this.mOrderInfo = mPoDal.GetProductOrderInfo(this.mLoginUser, this.mOrderCode, out iErrorMsg);

            if (!string.IsNullOrEmpty(iErrorMsg) || this.mOrderInfo == null)
            {
                iErrorMsg = "获取订单信息时出错";
                return result;
            }

            this.mStoreOutList = this.mSoDal.GetStoreOutInfo(this.mOrderCode, out iErrorMsg);

            if (!string.IsNullOrEmpty(iErrorMsg) || this.mOrderInfo == null)
            {
                iErrorMsg = "获取订单出库信息时出错";
                return result;
            }

            #region 返回到前端信息
            result = new JObject()
            {
                {"OrderCode",this.mOrderInfo.OrderCode},
                {"Retailer",this.mOrderInfo.Retailer},
                {"CreateDate",this.mOrderInfo.DateAdded},
                {"TotalAddress",this.mOrderInfo.Address},
                {"Receiver",this.mOrderInfo.Receiver},
                {"AreaCode",this.mOrderInfo.AreaCode},
                {"Phone",this.mOrderInfo.Phone},
                {"StoreOutInfo",this.GetStoreOutInfo()},
                {"ExpressList",this.GetExpressList()},
            };
            #endregion

            return result;
        }

        public JArray GetExpressList()
        {
            cc.dal.SysExpress expDal = new dal.SysExpress();
            List<SysExpressInfo> expList = expDal.GetAllExpressInfo();
            JArray result = new JArray();

            if (expList != null && expList.Count > 0)
            {
                foreach (var item in expList)
                {
                    result.Add(new JObject()
                    {
                        {"ExpressName",item.ExpressName},
                        {"ExpId",item.Id},
                    });
                }
            }

            return result;
        }

        public JArray GetStoreOutInfo()
        {
            JArray result = new JArray();
            if (this.mStoreOutList != null && this.mStoreOutList.Count > 0)
            {
                foreach (var item in this.mStoreOutList)
                {
                    if (item.DetailList != null && item.DetailList.Count > 0)
                    {
                        JArray jaDetail = new JArray();
                        foreach (var detail in item.DetailList)
                        {
                            string proName = (from t in this.mOrderInfo.DetailList
                                              where t.ProId == detail.ProId
                                              select t.ProName).FirstOrDefault();

                            jaDetail.Add(new JObject()
                            {
                                {"ProId",detail.ProId},
                                {"ProName",proName},
                                {"StoreNum",detail.ProCount},
                            });
                        }

                        result.Add(new JObject()
                        {
                            {"StoreOutNum",item.StoreCode},
                            {"StoreOutDate",item.DateAdded},
                            {"Status",item.Status},
                            {"Express",item.Express},
                            {"ExpressId",item.ExpressId},
                            {"ExpId",item.ExpId},
                            {"ExpCode",item.ExpCode},
                            {"ExpDateTime",item.ExpDateTime},
                            {"Detail",jaDetail},
                        });
                    }
                }
            }

            return result;
        }
        #endregion

        #region 卖家发货
        public bool SellerDeliverGoodsInfo(common.UserInfo iLoginUser, string iStoreOutCode, int iExpId, string iExpNum, out string iErrorMsg)
        {
            DataSet ds = null;
            cc.dal.StoreOut soDal = new dal.StoreOut();
            bool result = soDal.SellerDeliverGoodsInfo(iLoginUser, iStoreOutCode, iExpId, iExpNum, out ds, out iErrorMsg);

            #region 发货时通知买家
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                JArray orderInfo = new JArray();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string orderCode = cc.common.DataConvert.DataConvert.ToString(item["OrderCode"]);
                    string supplier = cc.common.DataConvert.DataConvert.ToString(item["Supplier"]);
                    string retailer = cc.common.DataConvert.DataConvert.ToString(item["Retailer"]);
                    string receiver = cc.common.DataConvert.DataConvert.ToString(item["Receiver"]);
                    string openid = cc.common.DataConvert.DataConvert.ToString(item["Openid"]);
                    string expressName = cc.common.DataConvert.DataConvert.ToString(item["ExpressName"]);
                    string empressCode = cc.common.DataConvert.DataConvert.ToString(item["EmpressCode"]);

                    orderInfo.Add(new JObject()
                        {
                            {"OrderCode",orderCode},
                            {"Supplier",supplier},
                            {"Retailer",retailer},
                            {"Receiver",receiver},
                            {"Openid",openid},
                            {"EmpressCode",empressCode},
                            {"ExpressName",expressName},
                        });
                }

                WeiXin.WeiXin weixin = new WeiXin.WeiXin();
                weixin.SendDeliverGoodsMsgToRetailerWx(orderInfo);
            }
            #endregion

            return result;
        }
        #endregion
    }
}
