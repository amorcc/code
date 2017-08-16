using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace cc.common.WebApiHelper
{
    public class WebApiHelper
    {
        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public static JObject Post(string url, string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //写数据
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "application/json";
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);

            //读数据
            request.Timeout = 300000;
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
            string strResult = streamReader.ReadToEnd();

            //关闭流
            reqstream.Close();
            streamReader.Close();
            streamReceive.Close();
            request.Abort();
            response.Close();

            //return JsonConvert.DeserializeObject<T>(strResult);
            return JObject.Parse(strResult);
        }

        public enum ApiType
        {
            UC = 1,
            PSI = 2,
            TMS = 3,
        }

        public static string GetApiUrl(ApiType iApiType)
        {
            string rt = string.Empty;
            switch (iApiType)
            {
                case ApiType.UC:
                    rt = ConfigurationManager.AppSettings["ucapiurl"] == null ? "" : ConfigurationManager.AppSettings["ucapiurl"].ToString();
                    break;
                case ApiType.PSI:
                    rt = ConfigurationManager.AppSettings["psiapiurl"] == null ? "" : ConfigurationManager.AppSettings["psiapiurl"].ToString();
                    break;
                case ApiType.TMS:
                    rt = ConfigurationManager.AppSettings["rcmapiurl"] == null ? "" : ConfigurationManager.AppSettings["rcmapiurl"].ToString();
                    break;
                default:
                    break;
            }

            return rt;
        }

        /// <summary>
        /// 与其他API进行通讯
        /// </summary>
        /// <param name="iApiType"></param>
        /// <param name="iUri"></param>
        /// <param name="iPara"></param>
        /// <returns></returns>
        public static JObject CallApi(ApiType iApiType, string iUri, string iPara)
        {
            string ucWebapiUrl = GetApiUrl(iApiType);

            JObject rt = cc.common.WebApiHelper.WebApiHelper.Post(ucWebapiUrl + iUri, iPara.ToString());
            return rt;
        }
    }
}
