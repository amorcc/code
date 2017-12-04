using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.utility
{
    /// <summary>
    /// 扩展函数
    /// </summary>
    public static class ExtensionMethods
    {
        #region JObject.GetValueExt<T>扩展
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
                    //if (t.GetType().Name == "String")
                    //{
                    //    foreach (var item in Commons.errWords)
                    //    {
                    //        if (t.ToString().ToLower().IndexOf(item) > -1)
                    //        {
                    //            t = defaultValue;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (t.GetType() == typeof(decimal))
                    //{
                    //    var val = double.Parse(t.ToString());
                    //    //t = (T)(Object)Math.Round(val, 2);
                    //}
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return t;
        }
        #endregion

        #region JObject.SetProperty<T>扩展
        /// <summary>
        /// JObject.SetProperty<T>扩展
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
        #endregion

        #region 合并2个JObject
        /// <summary>
        /// 合并2个JObject
        /// </summary>
        /// <param name="jObject"></param>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static JObject ConcatJObject(this JObject jObject, JObject objValue)
        {
            string[] fields = objValue.Properties().Select(item => item.Name.ToString()).ToArray();
            string[] values = objValue.Properties().Select(item => item.Value.ToString()).ToArray();
            for (int i = 0; i < fields.Length; i++)
            {
                jObject[fields[i]] = JToken.FromObject(values[i]);
            }
            return jObject;

        }
        #endregion
    }
}
