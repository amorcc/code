using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cc.common.Utility;
using System.IO;
using Newtonsoft.Json;
using cc.common;
using cc.common.core;

namespace cc.webapi.Controllers
{
    public class WXController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage H5Pay([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCodes = jsonPara.GetValueExt<string>("OrderCodes", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                string openid = jsonPara.GetValueExt<string>("Openid", "");
                int payType = jsonPara.GetValueExt<int>("PayType", 0);
                string rn = jsonPara.GetValueExt<string>("rn", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                string ip = HttpRequestMessageExtensions.GetClientIpAddress(this.Request);

                cc.iservices.IWeChat wechatBC = new cc.services.WeChat();
                var result = wechatBC.H5Pay(loginUser, openid, rn, orderCodes, payType, ip);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };

            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage WeChatPay([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string orderCodes = jsonPara.GetValueExt<string>("OrderCodes", "");
                string token = jsonPara.GetValueExt<string>("token", "");
                string openid = jsonPara.GetValueExt<string>("Openid", "");
                int payType = jsonPara.GetValueExt<int>("PayType", 0);
                string rn = jsonPara.GetValueExt<string>("rn", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IWeChat wechatBC = new cc.services.WeChat();
                var result = wechatBC.WeChatPay(loginUser, openid, rn, orderCodes, payType);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };

            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetWxJsApiParam([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string openid = jsonPara.GetValueExt<string>("Openid", "");
                int totalFee = jsonPara.GetValueExt<int>("TotalFee", 0);
                #endregion

                //cc.iservices.IUserAuth userAuthBC = new cc.services.UserAuth();
                //var result = userAuthBC.Login(userName, password, openid, access_token);
                cc.iservices.IWeChat wechatBC = new cc.services.WeChat();
                var result = wechatBC.GetWxJsApiParam(openid, totalFee);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };

            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage GetOpenId([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string code = jsonPara.GetValueExt<string>("code", "");
                #endregion

                string appid = "wx3241c2f4565fa11c";
                string secret = "ad692205c3d8e5068f88dc73d8728085";

                #region 到微信去获取openid
                var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);

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

                string openid = jo.GetValueExt<string>("openid", "");
                string access_token = jo.GetValueExt<string>("access_token", "");

                if (!string.IsNullOrEmpty(openid))
                {
                    string token = System.Guid.NewGuid().ToString("N");
                    cc.iservices.IUserAuth userBC = new cc.services.UserAuth();
                    var result1 = userBC.LoginByOpenid(openid);
                    if (result1.ResponseID == ResponseResult.Success && result1.Data != null)
                    {
                        return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result1), System.Text.Encoding.UTF8, "application/json") };
                    }
                }

                ActionResult<JObject> result = new ActionResult<JObject>();
                result.Message = "";
                result.ResponseID = ResponseResult.BusinessError;
                result.Data = new JObject()
                    {
                        {"Openid", openid},
                        {"access_token", access_token},
                    };

                cc.log.Log.Debug(typeof(WXController), null, string.Format("openid:{0},access_token:{1}", openid, access_token));

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };

            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpPost]
        public HttpResponseMessage SendToWxOrderCreateMsg([FromBody]JObject jsonPara)
        {
            try
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
                JObject user = new JObject()
                {
                    {"value","陈先生"},
                    {"color","#fff000"},
                };

                JObject orderID = new JObject()
                {
                    {"value","202154651321212"},
                    {"color","#173177"},
                };
                JObject orderMoneySum = new JObject()
                {
                    {"value","999"},
                    {"color","#173177"},
                };

                JObject backupFieldName = new JObject()
                {
                    {"value","backupFieldName"},
                    {"color","#173177"},
                };

                JObject backupFieldData = new JObject()
                {
                    {"value","backupFieldData"},
                    {"color","#173177"},
                };

                JObject remark = new JObject()
                {
                    {"value","这是测试"},
                    {"color","#173177"},
                };

                JObject data = new JObject()
                {
                    {"first",user},
                    {"orderID",orderID},
                    {"orderMoneySum",orderMoneySum},
                    {"backupFieldName",backupFieldName},
                    {"backupFieldData",backupFieldData},
                    {"remark",remark},
                };

                JObject toWxPara = new JObject()
                {
                    {"touser","o2eoPw-rtsG0ynrkbtOyk0BYdmKc"},
                    {"template_id","IvsaPItro4vCNwpkSucXpBTEsPFeNSu3mS_iqMkLWCI"},
                    {"url","http://m.tianxiajiancai.com.cn"},
                    {"topcolor","#FF0000"},
                    {"data",data},
                };

                string postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", access_token);
                JObject result = cc.common.WebApiHelper.WebApiHelper.Post(postUrl, toWxPara.ToString());

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(toWxPara), System.Text.Encoding.UTF8, "application/json") };

            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }

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
