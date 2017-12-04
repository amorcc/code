using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Data;

public static class ExtensionMethods
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

    /// 把 DataRow 转换成 Model实例 并返回 实例的 Json string 
    public static T ToModelInfoOf<T>(this DataRow modelDr)
    {
        var type = typeof(T);
        var t = (T)Activator.CreateInstance(type);
        var properties = type.GetProperties();
        System.IFormatProvider format = new System.Globalization.CultureInfo("zh-cn", true);
        foreach (var propertyInfo in properties)
        {
            try //有些列不需要返回到客户端,因此在返回的dataset里 没有这些列,用这个通用的方法去访问这个列的值会有异常,只需捕捉就行
            {
                var value = modelDr[propertyInfo.Name];

                if (value != DBNull.Value)
                {
                    if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        propertyInfo.SetValue(t,
                            value == DBNull.Value ? DateTime.MinValue : DateTime.Parse(value.ToString(), format), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(int))
                    {
                        propertyInfo.SetValue(t, int.Parse(value.ToString()), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(decimal))
                    {
                        propertyInfo.SetValue(t, decimal.Parse(value.ToString()), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(JArray))
                    {
                        propertyInfo.SetValue(t, JArray.Parse(value.ToString()), null);
                    }
                    else
                    {
                        propertyInfo.SetValue(t, value, null);
                    }
                }
            }
            catch
            {

            }
        }
        return t;
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
        var ret = GetValueExt<string>(jo, token, defaultValue, caseSensitive);

        if (!bMaskKeyWord)
        {
            return ret;
        }
        //if (!string.IsNullOrEmpty(ret))
        //{
        //    foreach (var item in Commons.errWords)
        //    {
        //        if (ret.IndexOf(item) > -1)
        //        {
        //            ret = Regex.Replace(ret, item, "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        //        }
        //    }
        //    return ret;//Common.maskKeyWord(ret, dicFile);
        //}
        return ret;

    }
}
