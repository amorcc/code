using cc.core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace cc.utility
{
    public class Common
    {

        public static string GetIp
        {
            get
            {
                try
                {
                    string ip;
                    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_CNET_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(ip))
                    {
                        ip = HttpContext.Current.Request.UserHostAddress;
                    }
                    return ip;
                }
                catch
                {
                    return "";
                }
            }
        }

        public static string App(string key)
        {
            return ConfigurationManager.AppSettings[key] == null ? "" : ConfigurationManager.AppSettings[key].ToString();
        }
        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="iErrorInfo">错误信息提示</param>
        /// <returns></returns>
        public static string ShowError(string iErrorInfo)
        {
            var Payload = new StandardPayload();
            Payload.ResponseID = 1;
            Payload.Message = iErrorInfo;
            return JsonConvert.SerializeObject(Payload);
        }
        /// <summary>
        /// 向某个Url以 Post的方式 发送数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postdata"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string PostPage(string url, string postdata, Encoding encode)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.01; Windows NT 5.0)";
                request.Timeout = 10000;

                #region 填充要post的内容
                if (postdata.Length > 0)
                {
                    request.ContentType = "application/Json";

                    request.Method = "Post";
                    byte[] data = encode.GetBytes(postdata);
                    request.ContentLength = data.Length;

                    Stream requestStream = request.GetRequestStream();

                    requestStream.Write(data, 0, data.Length);

                    requestStream.Close();
                }
                #endregion

                var response = request.GetResponse() as HttpWebResponse;
                Stream stream;
                if (response.ContentEncoding == "gzip") // 注意内容编码
                {
                    stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                }
                else
                {
                    stream = response.GetResponseStream();
                }
                var reader = new StreamReader(stream, encode);
                string text = reader.ReadToEnd();
                //把 responseText 和 postData log 起来


                reader.Close();
                response.Close();
                return text;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 生成N为随机数
        /// </summary>
        /// <param name="VcodeNum"></param>
        /// <returns></returns>
        public static string RndNum(int VcodeNum)
        {
            StringBuilder sb = new StringBuilder(VcodeNum);
            Random rand = new Random();
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                int t = rand.Next(9);
                sb.AppendFormat("{0}", t);
            }
            return sb.ToString();

        }

    }
}
