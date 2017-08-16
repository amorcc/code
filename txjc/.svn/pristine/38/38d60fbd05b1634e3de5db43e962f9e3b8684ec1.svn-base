using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cc.common.Utility;
using Newtonsoft.Json;

namespace cc.webapi.Controllers
{
    public class AreaMngController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetAreaInfo([FromBody]JObject jsonPara)
        {
            try
            {
                #region 解析json
                string pCodeStr = jsonPara.GetValueExt<string>("pcode", "-1");
                string codeStr = jsonPara.GetValueExt<string>("code", "-1");
                string levelStr = jsonPara.GetValueExt<string>("level", "-1");
                #endregion

                int pCode = -1;
                int code = -1;
                int level = -1;

                int.TryParse(pCodeStr, out  pCode);
                int.TryParse(codeStr, out  code);
                int.TryParse(levelStr, out  level);

                cc.iservices.IAreaMng areaBC = new cc.services.AreaMng();
                var result = areaBC.GetAreaInfo(pCode, level, code);

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
