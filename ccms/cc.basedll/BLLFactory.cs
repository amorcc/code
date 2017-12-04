using cc.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.basedll
{
    public class BLLFactory<T> where T : class
    {
        /// <summary>
        /// 创建或者从缓存中获取对应业务类的实例
        /// </summary>
        public static T Instance
        {
            get
            {
                return Reflect<T>.Create(typeof(T).Name, typeof(T).Namespace, true); //反射创建，并缓存
            }
        }

    }
}
