using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace cc.common.Cache
{
    /// <summary>
    /// 缓存管理
    /// cc 2017-3-17
    /// </summary>
    public class CacheMng<T>
    {
        static List<string> mCacheNameList = new List<string>();

        public static List<T> GetCache(string iCacheName)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            List<T> cache = objCache[iCacheName] as List<T>;

            if (cache != null && cache.Count > 0)
            {
                for (int i = cache.Count - 1; i >= 0; i--)
                {
                    T model = cache[i];

                    if (model is basemodel.BaseModel)
                    {
                        if ((model as basemodel.BaseModel).IsCacheTimeOut() == true)
                        {
                            cache.Remove(model);
                        }
                    }
                }
            }

            return cache;
        }

        public static void AddCache(string iCacheName, List<T> iCache)
        {
            List<string> find = (from t in mCacheNameList
                                 where t == iCacheName
                                 select t).ToList();

            if (find != null && find.Count > 0)
            {
                //throw new Exception(string.Format("zmm.common.CacheMng,已经存在该缓存信息!cacheName={0}", iCacheName));
                List<T> cache = GetCache(iCacheName);
                cache.AddRange(iCache);
            }
            else
            {
                mCacheNameList.Add(iCacheName);
                System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                objCache[iCacheName] = iCache;
            }
        }

        public static void RemoveCache(string iCacheName)
        {
            List<string> find = (from t in mCacheNameList
                                 where t == iCacheName
                                 select t).ToList();

            if (find != null && find.Count > 0)
            {
                mCacheNameList.Remove(iCacheName);
            }

            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(iCacheName);
        }
    }
}
