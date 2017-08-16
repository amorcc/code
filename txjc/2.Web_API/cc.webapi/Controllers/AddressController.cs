using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cc.common.Utility;
using cc.common;
using cc.common.core;

namespace cc.webapi.Controllers
{
    public class AddressController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UpdateAddress([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string AddressDetail = jsonPara.GetValueExt<string>("AddressDetail", "");
                string Receiver = jsonPara.GetValueExt<string>("Receiver", "");
                string Phone = jsonPara.GetValueExt<string>("Phone", "");
                int City = jsonPara.GetValueExt<int>("City", 0);
                int Province = jsonPara.GetValueExt<int>("Province", 0);
                int Zone = jsonPara.GetValueExt<int>("Zone", 0);
                string token = jsonPara.GetValueExt<string>("token", "");
                #endregion

                UserInfo loginUser = cc.common.TokenMng.TokenManage.GetUser(token);

                if (loginUser == null || loginUser.IsTimeOut() == true)
                {
                    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.MustLogin<MyEntity>()), System.Text.Encoding.UTF8, "application/json") };
                }

                cc.iservices.IAddressMng addressBC = new cc.services.AddressMng();
                var result = addressBC.Insert(loginUser, Province, City, Zone, Receiver, Phone, AddressDetail);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(UserAuthController), ex);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            }
        }
    }
}
