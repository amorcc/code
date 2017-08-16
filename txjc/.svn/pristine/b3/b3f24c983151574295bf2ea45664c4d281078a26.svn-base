using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace cc.unit.WeChat.Pay
{
    /// 支付结果通知回调处理类
    /// 负责接收微信支付后台发送的支付结果并对订单有效性进行验证，将验证结果反馈给微信支付后台
    /// </summary>
    public class ResultNotify : Notify
    {
        public ResultNotify(Page page)
            : base(page)
        {
        }

        public override void ProcessNotify()
        {
            WxPayData notifyData = GetNotifyData();

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                //Log.Error(this.GetType().ToString(), "The Pay result is error : " + res.ToXml());
                cc.log.Log.Error(this.GetType(), new Exception("The Pay result is error : " + res.ToXml()));
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();
            string rn = notifyData.GetValue("out_trade_no").ToString();
            string orderCodes = notifyData.GetValue("attach").ToString();

            cc.log.Log.Debug(this.GetType(), null, string.Format("微信支付回调,result={0}", notifyData.ToJson()));

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                //Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                cc.log.Log.Error(this.GetType(), new Exception("Order query failure : " + res.ToXml()));
                //page.Response.Write(res.ToXml());
                //page.Response.End();
            }
            //查询订单成功
            else
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");

                cc.log.Log.Debug(this.GetType(), null, string.Format("微信支付成功回调,rn={0}，OrderCodes={1}, result={2}", rn, orderCodes, notifyData.ToJson()));


                string errorMsg = string.Empty;
                DataSet ds = new DataSet();
                cc.dal.ProductOrder po = new dal.ProductOrder();
                po.OrderPay(orderCodes, 2, out ds, out errorMsg);

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
                            {"PayType",2},
                            {"PayTypeDesc",payTypeDesc},
                            {"Openid",openid},
                        });
                    }

                    WeiXin.WeiXin weixin = new WeiXin.WeiXin();
                    weixin.SendPayMsgToSupplierWx(orderInfo);
                    #endregion
                }
                //page.Response.Write(res.ToXml());
                //page.Response.End();
            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
