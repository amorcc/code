using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace cc.unit.WeChat.Pay
{
    public class PayMng
    {
        public JObject H5Pay(common.UserInfo iLoginUser, string iRN, string iOpenId, string iOrderCodes, int iPayType, string iIp, out string iErrorMsg)
        {
            //GetIP();
            iErrorMsg = string.Empty;

            cc.log.Log.Debug(this.GetType(), iLoginUser, string.Format("rn={0},opeind={1},ordercodes={2},paytype={3}", iRN, iOpenId, iOrderCodes, iPayType));

            #region 获取订单信息
            cc.unit.PayMng.PayMng pay = new unit.PayMng.PayMng();
            cc.unit.PayMng.PayBeforeOrderInfo orderInfo = pay.GetOrderInfoByPayBefore(iLoginUser, iRN, iOrderCodes, iPayType, out iErrorMsg);

            if (!string.IsNullOrEmpty(iErrorMsg))
            {
                return null;
            }
            #endregion

            string mweb_url = this.GetH5Param(iOpenId, Convert.ToInt32(orderInfo.Total * 100), iRN, iOrderCodes, iIp, out iErrorMsg);

            if (!string.IsNullOrEmpty(iErrorMsg))
            {
                return null;
            }

            return new JObject()
            {
                {"mweb_url", mweb_url},
            };
        }

        #region 获取客户端IP地址

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string tempIP = "";
            try
            {
                System.Net.WebRequest wr = System.Net.WebRequest.Create("http://city.ip138.com/ip2city.asp");
                System.IO.Stream s = wr.GetResponse().GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(s, System.Text.Encoding.GetEncoding("gb2312"));
                string all = sr.ReadToEnd(); //读取网站的数据

                int start = all.IndexOf("[") + 1;
                int end = all.IndexOf("]", start);
                tempIP = all.Substring(start, end - start);
                sr.Close();
                s.Close();
            }
            catch
            {
                if (System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Length > 1)
                    tempIP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1].ToString();
                if (string.IsNullOrEmpty(tempIP))
                    return GetIP();
            }
            return tempIP;
        }

        #endregion

        /// <summary>
        /// 微信公众号支付
        /// </summary>
        /// <param name="iLoginUser"></param>
        /// <param name="iOpenId"></param>
        /// <param name="iOrderCode"></param>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public JObject WeChatPay(common.UserInfo iLoginUser, string iRN, string iOpenId, string iOrderCodes, int iPayType, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;

            cc.log.Log.Debug(this.GetType(), iLoginUser, string.Format("rn={0},opeind={1},ordercodes={2},paytype={3}", iRN, iOpenId, iOrderCodes, iPayType));

            #region 获取订单信息
            cc.unit.PayMng.PayMng pay = new unit.PayMng.PayMng();
            cc.unit.PayMng.PayBeforeOrderInfo orderInfo = pay.GetOrderInfoByPayBefore(iLoginUser, iRN, iOrderCodes, iPayType, out iErrorMsg);

            if (!string.IsNullOrEmpty(iErrorMsg))
            {
                return null;
            }
            #endregion

            string wxJsApiParam = this.GetWxJsApiParam(iOpenId, Convert.ToInt32(orderInfo.Total * 100), iRN, iOrderCodes, out iErrorMsg);

            if (!string.IsNullOrEmpty(iErrorMsg))
            {
                return null;
            }

            return new JObject()
            {
                {"WxJsApiParam", wxJsApiParam},
            };
        }

        public string GetH5Param(string iOpenId, int iTotalFee, string iRN, string iOrderCodes, string iIp, out string iErrorMsg)
        {
            string wxJsApiParam = string.Empty;
            iErrorMsg = string.Empty;


            //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
            //JsApiPay jsApiPay = new JsApiPay(iOpenId, iTotalFee, iOrderCodes, iRN);
            H5Pay h5Pay = new H5Pay(iOpenId, iTotalFee, iOrderCodes, iRN, iIp);

            //JSAPI支付预处理
            try
            {
                WxPayData unifiedOrderResult = h5Pay.GetUnifiedOrderResult();
                //wxJsApiParam = h5Pay.GetJsApiParameters();//获取H5调起JS API参数                    
                ////Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                ////在页面上显示订单信息
                //return wxJsApiParam;
                string mweb_url = unifiedOrderResult.GetValue("mweb_url").ToString();

                return mweb_url;

            }
            catch (Exception ex)
            {
                //Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                //submit.Visible = false;
                iErrorMsg = "支付失败,请返回重试!msg =" + ex.ToString();
            }
            return "";
        }

        public string GetWxJsApiParam(string iOpenId, int iTotalFee, string iRN, string iOrderCodes, out string iErrorMsg)
        {
            string wxJsApiParam = string.Empty;
            iErrorMsg = string.Empty;

            //检测是否给当前页面传递了相关参数
            if (string.IsNullOrEmpty(iOpenId) || iTotalFee <= 0)
            {
                iErrorMsg = "必须正确传递openid和金额信息";
                return "";
            }

            //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
            JsApiPay jsApiPay = new JsApiPay(iOpenId, iTotalFee, iOrderCodes, iRN);

            //JSAPI支付预处理
            try
            {
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数                    
                //Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                //在页面上显示订单信息
                return wxJsApiParam;

            }
            catch (Exception ex)
            {
                //Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
                //submit.Visible = false;
                iErrorMsg = "支付失败,请返回重试!msg =" + ex.ToString();
            }
            return "";
        }

    }
}
