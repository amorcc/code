using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace cc.core
{
    /// <summary>
    /// 存储过程中返回msg,认为是业务逻辑验证不通过,需要通知用户,但是如果msg是以err:开头,则任务是 t sql 异常,需要记录
    /// </summary>
    [Serializable]
    public sealed class BusinessRullException : Exception
    {
        public BusinessRullException(string msg)
            : base(msg)
        {
            
        }
    }


    /// <summary>
    /// 在调用Proc的过程中,TSql出现异常,此时的msg是以err:打头
    /// </summary>
    [Serializable]
    public sealed class TSqlException : Exception
    {

        public TSqlException(string msg)
            : base(msg)
        {
            
        }
    }


}
