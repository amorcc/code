using cc.basedal;
using cc.basemodel;
using cc.utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.basebll
{
    public class BaseBLL<T, V> where V : BaseModel, new()
    {
        BaseDAL<V> baseDal;
        public BaseBLL()
        {
            baseDal = Reflect<BaseDAL<V>>.Create(typeof(T).Name, typeof(T).Namespace);
        }

        public bool Insert(V obj, SqlTransaction tran = null)
        {
            return baseDal.Insert(obj, tran);
        }

        public bool Update(V obj, SqlTransaction tran = null)
        {
            return baseDal.Update(obj, tran);
        }

        public V FindByPrimaryKey(string primaryKeyValue, SqlTransaction tran = null)
        {
            return baseDal.FindByPrimaryKey(primaryKeyValue, tran);
        }

        public List<V> List(string condition, SqlTransaction tran = null)
        {
            return baseDal.List(condition, tran);
        }

        public V FindByCondition(string condition, SqlTransaction tran = null)
        {
            return baseDal.FindByCondition(condition, tran);
        }
    }
}
