using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace cc.utility
{
    public class Reflect<T> where T : class
    {
        private static Hashtable m_objCache = null;
        public static Hashtable ObjCache
        {
            get
            {
                if (m_objCache == null)
                {
                    m_objCache = new Hashtable();
                }
                return m_objCache;
            }
        }

        public static T Create(string sName, string sFilePath)
        {
            return Create(sName, sFilePath, true);
        }
        /// <summary>
        /// 返回实例化的对象
        /// </summary>
        /// <param name="sName">Class名称</param>
        /// <param name="sFilePath">命名空间</param>
        /// <param name="bCache">是否缓存</param>
        /// <returns></returns>
        public static T Create(string sName, string sFilePath, bool bCache)
        {
            string CacheKey = sFilePath + "." + sName;
            T objType = null;
            try
            {
                if (bCache)
                {
                    objType = (T)ObjCache[CacheKey];    //从缓存读取 
                    if (!ObjCache.ContainsKey(CacheKey))
                    {
                        try
                        {
                            Assembly assObj = CreateAssembly(sFilePath);
                            object obj = assObj.CreateInstance(CacheKey);
                            objType = (T)obj;
                            ObjCache.Add(CacheKey, objType);// 写入缓存 将DAL内某个对象装入缓存
                        }
                        catch { }
                    }
                }
                else
                {
                    objType = (T)CreateAssembly(sFilePath).CreateInstance(CacheKey); //反射创建 
                    //objType = (T)CreateAssembly(sFilePath).CreateInstance(CacheKey, true, BindingFlags.Default, null, args, null, null);
                }
            }
            catch { }
            return objType;
        }

        /// <summary>
        /// DLL文件            player1 = (Player)asm.CreateInstance("DecorationClass." + ClassName, true, BindingFlags.Default, null, ObjArray, null, null);  
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
        public static Assembly CreateAssembly(string sFilePath)
        {
            Assembly assObj = (Assembly)ObjCache[sFilePath];
            if (assObj == null)
            {
                assObj = Assembly.Load(sFilePath);
                ObjCache.Add(sFilePath, assObj);//将整个DLL装入缓存
            }
            return assObj;
        }
    }
}
