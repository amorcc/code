using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cc.utility;

namespace cc.webapi.Controllers
{
    public class TestController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CreateQCcode([FromBody]JObject jsonPara)
        {
            string ip = HttpRequestMessageExtensions.GetClientIpAddress(this.Request);


            return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(ip), System.Text.Encoding.UTF8, "application/json") };
            //try
            //{
            //    #region 解析json
            //    string content = jsonPara.GetValueExt<string>("content", "");
            //    string filePath = jsonPara.GetValueExt<string>("FilePath", "");
            //    string token = jsonPara.GetValueExt<string>("token");
            //    #endregion

            //    cc.common.QRCode.QRCode qrcode = new common.QRCode.QRCode();

            //    var result = qrcode.CreateQCcode(content, filePath) ? 1 : 0;

            //    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            //}
            //catch (Exception ex)
            //{
            //    cc.log.Log.Error(typeof(UserAuthController), ex);
            //    return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(cc.common.Utility.MyResponse.ShowError<cc.common.core.MyEntity>(ex.ToString())), System.Text.Encoding.UTF8, "application/json") };
            //}
        }
    }
}
