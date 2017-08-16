using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using cc.utility;
using cc.unit.ProductOrder.Create;

namespace cc.unit.WeiXin
{
    public class WeiXin
    {
        #region 向微信发送发货成功消息
        public void SendDeliverGoodsMsgToRetailerWx(JArray iOrderArray)
        {
            try
            {
                if (iOrderArray != null && iOrderArray.Count > 0)
                {
                    string accessToken = this.GetAccessToken();

                    foreach (JObject item in iOrderArray)
                    {
                        JObject wxPara = GetDeliverGoodsMsgWxPara(item);
                        string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", accessToken);
                        JObject result = cc.common.WebApiHelper.WebApiHelper.Post(postUrl, wxPara.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        JObject GetDeliverGoodsMsgWxPara(JObject iOrderInfo)
        {
            string orderCode = iOrderInfo.GetValueExt<string>("OrderCode");
            string supplier = iOrderInfo.GetValueExt<string>("Supplier");
            string retailer = iOrderInfo.GetValueExt<string>("Retailer");
            string receiver = iOrderInfo.GetValueExt<string>("Receiver");
            string empressCode = iOrderInfo.GetValueExt<string>("EmpressCode");
            string expressName = iOrderInfo.GetValueExt<string>("ExpressName");
            string openId = iOrderInfo.GetValueExt<string>("Openid");

            string title = string.Format("尊敬的{0},收到一个来自{1}的发货通知，请注意查收！", retailer, supplier);
            string remarkStr = string.Format("快递公司：{0}", expressName);



            JObject user = new JObject()
                        {
                            {"value",title},
                            {"color","#173177"},
                        };

            JObject order_id = new JObject()
                        {
                            {"value",orderCode},
                            {"color","#173177"},
                        };
            JObject package_id = new JObject()
                        {
                            {"value",empressCode},
                            {"color","#173177"},
                        };


            JObject remark = new JObject()
                        {
                            {"value",remarkStr},
                            {"color","#173177"},
                        };

            JObject data = new JObject()
                        {
                            {"first",user},
                            {"order_id",order_id},
                            {"package_id",package_id},
                            {"remark",remark},
                        };


            JObject toWxPara = new JObject()
                        {
                            {"touser",openId},
                            {"template_id","0bK0ymQmxb9geX-xgPTC15BsbEnX-zA0worbzNLIRuE"},
                            {"url",cc.utility.Common.App("WapSiteUrl")+ "#/bol/4"},
                            {"topcolor","#FF0000"},
                            {"data",data},
                        };

            return toWxPara;
        }
        #endregion
        #region 向微信发送支付成功消息
        public void SendPayMsgToSupplierWx(JArray iOrderArray)
        {
            try
            {
                if (iOrderArray != null && iOrderArray.Count > 0)
                {
                    string accessToken = this.GetAccessToken();

                    foreach (JObject item in iOrderArray)
                    {
                        JObject wxPara = GetPayMsgWxPara(item);
                        string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", accessToken);
                        JObject result = cc.common.WebApiHelper.WebApiHelper.Post(postUrl, wxPara.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        JObject GetPayMsgWxPara(JObject iOrderInfo)
        {
            string orderCode = iOrderInfo.GetValueExt<string>("OrderCode");
            string supplier = iOrderInfo.GetValueExt<string>("Supplier");
            string message = iOrderInfo.GetValueExt<string>("Message");
            string retailer = iOrderInfo.GetValueExt<string>("Retailer");
            string totalPrice = iOrderInfo.GetValueExt<string>("TotalPrice");
            string payType = iOrderInfo.GetValueExt<string>("PayType");
            string payTypeDesc = iOrderInfo.GetValueExt<string>("PayTypeDesc");
            string openId = iOrderInfo.GetValueExt<string>("Openid");

            string title = string.Format("尊敬的{0},收到一个来自\"{2}\"{1}的订单支付通知，请尽快处理!", supplier, payTypeDesc, retailer);
            string remarkStr = string.Format("客户留言：{0}", message);

            if (string.IsNullOrEmpty(message))
            {
                remarkStr = "";
            }

            JObject user = new JObject()
                        {
                            {"value",title},
                            {"color","#173177"},
                        };

            JObject orderProductName = new JObject()
                        {
                            {"value","订单号"+orderCode},
                            {"color","#173177"},
                        };
            JObject orderMoneySum = new JObject()
                        {
                            {"value","￥"+totalPrice},
                            {"color","#173177"},
                        };


            JObject remark = new JObject()
                        {
                            {"value",remarkStr},
                            {"color","#173177"},
                        };

            JObject data = new JObject()
                        {
                            {"first",user},
                            {"orderProductName",orderProductName},
                            {"orderMoneySum",orderMoneySum},
                            {"remark",remark},
                        };


            JObject toWxPara = new JObject()
                        {
                            {"touser",openId},
                            {"template_id","AUYiv-ra63i9cTrtLFIlbD2oT87F5HfX3CUCV7WP00Q"},
                            {"url",cc.utility.Common.App("WapSiteUrl")+ "#/sol/1"},
                            {"topcolor","#FF0000"},
                            {"data",data},
                        };

            return toWxPara;
        }
        #endregion

        #region 向微信公众号发送下单成功消息
        public bool SendOrderCreateMsgToRetailerWx(string iAccessToken, string iUserName, string iOpenid, string iOrderCode, decimal iMoney, string iRemark)
        {
            if (!string.IsNullOrEmpty(iOpenid))
            {

                JObject user = new JObject()
                {
                    {"value",string.Format("尊敬的{0}，您的订单已提交成功，请尽快到平台支付{1}元",iUserName,iMoney.ToString("0.00"))},
                    {"color","#173177"},
                };

                JObject orderID = new JObject()
                {
                    {"value",iOrderCode},
                    {"color","#173177"},
                };
                JObject orderMoneySum = new JObject()
                {
                    {"value","￥"+iMoney.ToString("0.00")},
                    {"color","#173177"},
                };

                //JObject backupFieldName = new JObject()
                //    {
                //        {"value","支付方式："},
                //        {"color","#173177"},
                //    };

                //JObject backupFieldData = new JObject()
                //    {
                //        {"value",iPayTypeStr},
                //        {"color","#173177"},
                //    };

                JObject remark = new JObject()
                {
                    {"value",iRemark},
                    {"color","#173177"},
                };

                JObject data = new JObject()
                {
                    {"first",user},
                    {"orderID",orderID},
                    {"orderMoneySum",orderMoneySum},
                    //{"backupFieldName",backupFieldName},
                    //{"backupFieldData",backupFieldData},
                    {"remark",remark},
                };

                JObject toWxPara = new JObject()
                {
                    {"touser",iOpenid},
                    {"template_id","IvsaPItro4vCNwpkSucXpBTEsPFeNSu3mS_iqMkLWCI"},
                    {"url",cc.utility.Common.App("WapSiteUrl")+ "#/bol/1"},
                    {"topcolor","#FF0000"},
                    {"data",data},
                };

                string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", iAccessToken);
                JObject result = cc.common.WebApiHelper.WebApiHelper.Post(postUrl, toWxPara.ToString());

                return true;
            }

            return false;
        }

        public bool SendOrderCreateMsgToSupplierWx(List<SendOrderCreateInfo> iOrderInfo, string iAccessToken)
        {
            try
            {
                if (iOrderInfo != null && iOrderInfo.Count > 0)
                {
                    foreach (var item in iOrderInfo)
                    {
                        if (!string.IsNullOrEmpty(item.Openid))
                        {
                            string title = string.Format("尊敬的用户，{0}提交了新的订单，请注意查收！", item.RetailerName);

                            JObject wxPara = this.GetOrderCreateWxPara(item.Openid, iAccessToken, title, item.OrderCode, item.Total, "下单商家：", item.RetailerName, string.Format("下单时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

                            string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", iAccessToken);
                            JObject result = cc.common.WebApiHelper.WebApiHelper.Post(postUrl, wxPara.ToString());
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        JObject GetOrderCreateWxPara(string iOpenid, string iAccessToken, string iTitle, string iOrderCode, decimal iMoney, string iFieldName, string iFieldData, string iRemark)
        {
            JObject user = new JObject()
                        {
                            {"value",iTitle},
                            {"color","#173177"},
                        };

            JObject orderID = new JObject()
                        {
                            {"value",iOrderCode},
                            {"color","#173177"},
                        };
            JObject orderMoneySum = new JObject()
                        {
                            {"value","￥"+iMoney.ToString("0.00")},
                            {"color","#173177"},
                        };


            JObject remark = new JObject()
                        {
                            {"value",iRemark},
                            {"color","#173177"},
                        };

            JObject data = new JObject()
                        {
                            {"first",user},
                            {"orderID",orderID},
                            {"orderMoneySum",orderMoneySum},
                            {"remark",remark},
                        };

            if (!string.IsNullOrEmpty(iFieldName) && !string.IsNullOrEmpty(iFieldData))
            {
                JObject backupFieldName = new JObject()
                        {
                            {"value",iFieldName},
                            {"color","#173177"},
                        };

                JObject backupFieldData = new JObject()
                        {
                            {"value",iFieldData},
                            {"color","#173177"},
                        };

                data.SetProperty("backupFieldName", backupFieldName);
                data.SetProperty("backupFieldData", backupFieldData);
            }

            JObject toWxPara = new JObject()
                        {
                            {"touser",iOpenid},
                            {"template_id","IvsaPItro4vCNwpkSucXpBTEsPFeNSu3mS_iqMkLWCI"},
                            {"url",cc.utility.Common.App("WapSiteUrl")+ "#/sol/1"},
                            {"topcolor","#FF0000"},
                            {"data",data},
                        };

            return toWxPara;
        }

        public bool SendOrderCreateMsgToSupplierWx(string iAccessToken, List<string> iSupplerOpenidList, string iRetailerName, string iOrderCode, decimal iMoney, string iRemark)
        {
            if (iSupplerOpenidList != null && iSupplerOpenidList.Count > 0)
            {
                foreach (var item in iSupplerOpenidList)
                {

                    string iOpenid = item;
                    if (!string.IsNullOrEmpty(iOpenid))
                    {

                        JObject user = new JObject()
                        {
                            {"value",string.Format("尊敬的用户，{0}提交了新的订单，请注意查收！",iRetailerName)},
                            {"color","#173177"},
                        };

                        JObject orderID = new JObject()
                        {
                            {"value",iOrderCode},
                            {"color","#173177"},
                        };
                        JObject orderMoneySum = new JObject()
                        {
                            {"value","￥"+iMoney.ToString("0.00")},
                            {"color","#173177"},
                        };

                        JObject backupFieldName = new JObject()
                        {
                            {"value","下单用户："},
                            {"color","#173177"},
                        };

                        JObject backupFieldData = new JObject()
                        {
                            {"value",iRetailerName},
                            {"color","#173177"},
                        };

                        JObject remark = new JObject()
                        {
                            {"value",iRemark},
                            {"color","#173177"},
                        };

                        JObject data = new JObject()
                        {
                            {"first",user},
                            {"orderID",orderID},
                            {"orderMoneySum",orderMoneySum},
                            //{"backupFieldName",backupFieldName},
                            //{"backupFieldData",backupFieldData},
                            {"remark",remark},
                        };

                        JObject toWxPara = new JObject()
                        {
                            {"touser",iOpenid},
                            {"template_id","IvsaPItro4vCNwpkSucXpBTEsPFeNSu3mS_iqMkLWCI"},
                            {"url",cc.utility.Common.App("WapSiteUrl")+ "#/bol/1"},
                            {"topcolor","#FF0000"},
                            {"data",data},
                        };

                        string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", iAccessToken);
                        JObject result = cc.common.WebApiHelper.WebApiHelper.Post(postUrl, toWxPara.ToString());

                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 获取微信的access_token
        public string GetAccessToken()
        {
            string appid = this.GetAppid();
            string secret = this.GetSecret();
            #region 到微信去获取openid
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream ioStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(ioStream, System.Text.UTF8Encoding.UTF8);
            string html = sr.ReadToEnd();
            sr.Close();
            ioStream.Close();
            response.Close();
            #endregion

            JObject jo = JObject.Parse(html);

            string access_token = jo.GetValueExt<string>("access_token", "");

            return access_token;
        }
        #endregion

        string GetAppid()
        {
            return cc.utility.Common.App("appid");
        }

        string GetSecret()
        {
            return cc.utility.Common.App("secret");
        }
    }
}
