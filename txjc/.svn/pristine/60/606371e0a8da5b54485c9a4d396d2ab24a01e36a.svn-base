using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Globalization;
using System.Net.Http;

namespace cc.utility
{
    public static class MyExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jObject"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static JObject SetProperty<T>(this JObject jObject, string property, T value)
        {
            jObject[property] = JToken.FromObject((T)value);
            return jObject;
        }
        /// <summary>
        /// 获取某个对象的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public static string GetPropertyValue<T>(this T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return String.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return String.Empty;

            return o.ToString();
        }

        //added by ldf 04212016 Unix 时间戳  +8 区
        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            //var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            //return Convert.ToInt64((dateTime - start).TotalSeconds);
            long intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (long)(dateTime - startTime).TotalSeconds;
            return intResult;
        }

        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        /// <param name="unixTimeStamp">时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(this DateTime target, long timestamp)
        {
            //var start = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            //return start.AddSeconds(timestamp);
            System.DateTime time = target;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(timestamp);
            return time;
        }

        /// <summary>
        /// 从一个JObject里获取对应属性的值.返回值里会被顾虑关键字,比如防止sql注入,javascript,敏感词等等
        /// </summary>
        /// <typeparam name="T">要返回的数据类型,默认是string</typeparam>
        /// <param name="jo">要从哪个JObject里获取值</param>
        /// <param name="token">当前JObject的某个属性</param>
        /// <param name="caseSensitive">token的名称是否区分大小写,比如 A a,默认不区分大小写</param>
        /// <param name="defaultValue">如果要查找的属性不存在,需要什么样的默认值</param>
        /// <returns>返回这个T类型的数据</returns>
        public static T GetValueExt<T>(this JObject jo, string token, T defaultValue = default(T), bool caseSensitive = false)
        {
            T t;
            try
            {
                //判断当前jo是否含有 token对应的属性
                var curPro =
                    jo.Properties()
                        .FirstOrDefault(x => String.Equals(x.Name, token, StringComparison.CurrentCultureIgnoreCase));
                if (caseSensitive)
                {
                    curPro =
                    jo.Properties()
                        .FirstOrDefault(x => String.Equals(x.Name, token));
                }
                if (curPro == null) //没有这个token
                {
                    t = defaultValue;
                }
                else
                {
                    t = jo.Value<T>(curPro.Name);

                    if (typeof(T).ToString().ToLower() == "system.string")
                    {
                        if (t != null)
                        {
                            return (T)Convert.ChangeType(t.ToString().Replace("'", "''"), typeof(T), CultureInfo.InvariantCulture);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return t;
        }

        /// <summary>
        /// 从一个JObject里获取对应属性的值.返回值里会被顾虑关键字,比如防止sql注入,javascript,敏感词等等
        /// </summary>
        /// <param name="jo">要返回的数据类型,默认是string</param>
        /// <param name="token">要从哪个JObject里获取值</param>
        /// <param name="caseSensitive">token的名称是否区分大小写,比如 A a,默认不区分大小写</param>
        /// <param name="defaultValue">如果要查找的属性不存在,需要什么样的默认值</param>
        /// <param name="bMaskKeyWord">是否需要进行关键字过滤,默认是需要</param>
        /// <param name="dicFile">对于获取string类型的值,可以传一个字典地址来过滤敏感词,关键字</param>
        /// <returns>返回找到的数据</returns>
        public static string GetValueExt(this JObject jo, string token, string defaultValue = "", bool caseSensitive = false, bool bMaskKeyWord = true, string dicFile = "")
        {
            var ret = GetValueExt<string>(jo, token, defaultValue, caseSensitive).Replace("'", "''");
            if (!bMaskKeyWord)
            {
                return ret;
            }
            if (!string.IsNullOrEmpty(ret))
            {
                return maskKeyWord(ret, dicFile);
            }
            return ret;

        }


        /// <summary>
        /// 过滤非法词汇.比如javascript, sql 关键字, 敏感词
        /// </summary>
        /// <param name="source">需要处理的字符串</param>
        /// <param name="dicPath">敏感词字典文件,文本文件 | 分隔,敏感词会被替换为 ***</param>
        /// <returns></returns>
        private static string maskKeyWord(string source, string dicFile = "")
        {
            var ret = source;
            var dicFilePath = string.Empty;
            if (string.IsNullOrEmpty(dicFile))
            {
                try
                {

                    var file = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(".exe", "_dic.txt");
                    if (File.Exists(file))
                    {
                        return file;
                    }
                }
                catch (Exception)
                {
                }

            }
            else
            {
                dicFilePath = dicFile;
            }
            if (string.IsNullOrEmpty(dicFilePath))
            {
                ret = source;
            }
            if (File.Exists(dicFilePath))
            {
                var maskPattern = string.Empty;
                using (var sr = new StreamReader(dicFilePath, System.Text.Encoding.UTF8))
                {
                    maskPattern = sr.ReadToEnd(); //  | 分隔的
                    sr.Close();
                    if (!string.IsNullOrEmpty(maskPattern))
                    {
                        ret = Regex.Replace(source, maskPattern, "***", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    }
                }
            }

            return ret;
        }


        //added by ldf 03152016
        /// <summary>
        /// 把一个DataRow 转换成一个JObject
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="hideFrom">整数,默认0,如果设置一个大于0的数,则从这列后面的不在转入Jobject</param>
        /// <returns>返回一个Object { {colName,colValue} ,{colName,colValue} }</returns>
        public static JObject ToJObject(this DataRow dr, int hideFrom = 0)
        {
            var jo = new JObject();
            var cols = dr.Table.Columns;
            if (hideFrom <= 0)
            {
                hideFrom = cols.Count;
            }
            var col1 = new Dictionary<string, Type>();
            for (int i = 0; i < hideFrom; i++)
            {
                col1.Add(cols[i].ColumnName, cols[i].DataType);
            }
            foreach (var col in col1.Keys)
            {
                var val = string.Empty;
                if (dr[col] != DBNull.Value)
                {
                    val = dr[col].ToString();
                }
                else
                {
                    var type = col1[col].ToString().ToLower();
                    switch (type)
                    {
                        case "system.datetime":
                            val = string.Empty;//updated by ldf 11082016 如果数据库时间是DBNull,赋值空
                            break;
                        case "system.string":
                            val = string.Empty;
                            break;
                        case "system.byte":
                            val = "0";
                            break;
                        case "system.int32":
                            val = "0";
                            break;
                        case "system.decimal":
                            val = "0.00";
                            break;
                        default:
                            break;
                    }
                }
                jo.Add(col.ToLower(), val);
            }

            return jo;
        }

        #region 获取客户端IP
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage =
            "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            // Web-hosting. Needs reference to System.Web.dll
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting. Needs reference to System.ServiceModel.dll. 
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
            if (request.Properties.ContainsKey(OwinContext))
            {
                dynamic owinContext = request.Properties[OwinContext];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }

            return null;
        }
        #endregion

    }
}
