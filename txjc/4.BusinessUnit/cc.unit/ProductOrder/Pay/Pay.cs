using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.ProductOrder.Pay
{
    public class Pay
    {
        public bool OrderPay(cc.common.UserInfo iLoginUser, string iOrderCodes, int iPayType, out string iErrorMsg)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            DataSet ds = null;
            bool result = poDal.OrderPay(iOrderCodes, iPayType, out ds, out iErrorMsg);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                #region 发送支付信息到卖家的微信

                JArray orderInfo = new JArray();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    string orderCode = cc.common.DataConvert.DataConvert.ToString(item["OrderCode"]);
                    string supplier = cc.common.DataConvert.DataConvert.ToString(item["Supplier"]);
                    string message = cc.common.DataConvert.DataConvert.ToString(item["Message"]);
                    string retailer = cc.common.DataConvert.DataConvert.ToString(item["Retailer"]);
                    string totalPrice = cc.common.DataConvert.DataConvert.ToString(item["TotalPrice"]);
                    string payTypeDesc = cc.common.DataConvert.DataConvert.ToString(item["PayTypeDesc"]);
                    string openid = cc.common.DataConvert.DataConvert.ToString(item["Openid"]);

                    orderInfo.Add(new JObject()
                        {
                            {"OrderCode",orderCode},
                            {"Supplier",supplier},
                            {"Message",message},
                            {"Retailer",retailer},
                            {"TotalPrice",totalPrice},
                            {"PayType",iPayType},
                            {"PayTypeDesc",payTypeDesc},
                            {"Openid",openid},
                        });
                }

                WeiXin.WeiXin weixin = new WeiXin.WeiXin();
                weixin.SendPayMsgToSupplierWx(orderInfo);
                #endregion
            }

            return result;
        }
    }
}
