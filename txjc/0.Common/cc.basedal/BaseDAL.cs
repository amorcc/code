using HX.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using cc.basemodel;
using cc.common.Utility;

namespace cc.basedal
{
    public class BaseDAL<T> where T : BaseModel, new()
    {

        protected string _sqlCon;//数据连接
        protected string _tableName;//需要初始化的对象表名
        protected string _primaryKey = "id";//数据库的主键字段名
        protected string _sortField = "DateAdded";//排序字段
        protected bool _isDesc = true;//


        #region 增、删、改通用操作方

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="jsonPara">写入参数</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns>
        public bool Insert(string jsonPara, SqlTransaction tran = null)
        {
            var result = false;
            var jo = JObject.Parse(jsonPara);
            jo.SetProperty("DateAdded", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //如果没有参数直接返回
            if (jo.Count == 0)
                return result;
            string[] fields = jo.Properties().Select(item => item.Name.ToString()).ToArray();
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            var parmStr = new List<string>();
            SqlParameter[] param = new SqlParameter[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                parmStr.Add(string.Format("@{0}", fields[i]));
                param[i] = new SqlParameter("@" + fields[i], values[i]);
            }
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", this._tableName, string.Join(",", fields), string.Join(",", parmStr));
            if (tran == null)
                result = SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.Text, sql, param) > 0;
            else
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, param) > 0;
            return result;
        }

        /// <summary>
        /// 更新某个表一条记录
        /// </summary>
        /// <param name="jsonPara">参数</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns>
        public bool Update(string jsonPara, SqlTransaction tran = null)
        {
            var result = false;
            var jo = JObject.Parse(jsonPara);
            //更新时至少有2个参数，主键和需要修改的值，否则直接返回
            if (jo.Count < 2)
                return result;
            //获取本次更新的主键值
            var primaryValue = jo[_primaryKey] == null ? "" : jo[_primaryKey].Value<string>();
            if (primaryValue == "")
                return result;
            string[] fields = jo.Properties().Select(item => item.Name.ToString()).ToArray();
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            var updateArr = new List<string>();
            SqlParameter[] param = new SqlParameter[fields.Length - 1];
            for (int i = 1; i < fields.Length; i++)
            {
                updateArr.Add(string.Format("{0} = @{0}", fields[i]));
                param[i - 1] = new SqlParameter("@" + fields[i], values[i]);
            }
            string sql = string.Format("UPDATE {0} SET {1} WHERE {2} = '{3}' ", this._tableName, string.Join(",", updateArr), _primaryKey, primaryValue);
            if (tran == null)
                result = SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.Text, sql, param) > 0;
            else
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, param) > 0;
            return result;
        }

        /// <summary>
        /// 更新某个表一条记录
        /// </summary>
        /// <param name="jsonPara">参数</param>
        /// <param name="condition">条件</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns>
        public int UpdateByCondition(string jsonPara, string condition, SqlTransaction tran = null)
        {
            var result = 0;
            var jo = JObject.Parse(jsonPara);
            //更新时至少有1个参数，主键和需要修改的值，否则直接返回
            if (jo.Count < 1)
                return result;
            ////获取本次更新的主键值
            //var primaryValue = jo[_primaryKey] == null ? "" : jo[_primaryKey].Value<string>();
            //if (primaryValue == "")
            //    return result;
            string[] fields = jo.Properties().Select(item => item.Name.ToString()).ToArray();
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            var updateArr = new List<string>();
            SqlParameter[] param = new SqlParameter[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                updateArr.Add(string.Format("{0} = @{0}", fields[i]));
                param[i] = new SqlParameter("@" + fields[i], values[i]);
            }
            string sql = string.Format("UPDATE {0} SET {1} {2} ", this._tableName, string.Join(",", updateArr), condition);
            if (tran == null)
                result = SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.Text, sql, param);
            else
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, param);
            return result;
        }

        /// <summary>
        /// 根据主键,从数据库中删除指定对象
        /// </summary>
        /// <param name="primaryKeyValue">主键值</param>
        /// <param name="tran">事务</param>
        /// <returns>是否成功</returns>
        public bool Delete(string primaryKeyValue, SqlTransaction tran = null)
        {
            var result = false;
            if (string.IsNullOrEmpty(primaryKeyValue))
                return result;
            var param = new[] { new SqlParameter("@" + _primaryKey, primaryKeyValue) };
            var sql = string.Format("DELETE FROM dbo.{0} WHERE {1}=@{1} ", _tableName, _primaryKey);
            if (tran == null)
                result = SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.Text, sql, param) > 0;
            else
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, param) > 0;
            return result;

        }

        public int DeleteByCondition(string condition, SqlTransaction tran = null)
        {
            if (condition == "")
                return 0;
            var sql = string.Format("DELETE FROM dbo.{0} {1}", _tableName, condition);
            if (tran == null)
                return SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.Text, sql, null);
            else
                return SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);

        }

        #endregion

        /// <summary>
        /// 用主键查询对象(用于字符型主键)
        /// </summary>
        /// <param name="primaryKeyValue">主键值</param>
        /// <param name="tran">事务</param>
        /// <returns>指定的对象</returns>
        public int GetCount(string condition, SqlTransaction tran = null)
        {
            int count = 0;
            string sql = string.Format("Select count(1) AS rc From dbo.{0} {1}", _tableName, condition);
            if (tran == null)
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(this._sqlCon, CommandType.Text, sql, null))
                {
                    if (dr.Read())
                    {
                        count = Convert.ToInt32(dr["rc"] == DBNull.Value ? 0 : dr["rc"]);
                    }
                }
            }
            else
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null);
                if (dr.Read())
                {
                    count = Convert.ToInt32(dr["rowCount"] == DBNull.Value ? 0 : dr["rowCount"]);
                }
            }
            return count;
        }

        #region 返回对象集合
        /// <summary>
        /// 用主键查询对象(用于字符型主键)
        /// </summary>
        /// <param name="primaryKeyValue">主键值</param>
        /// <param name="tran">事务</param>
        /// <returns>指定的对象</returns>
        public T FindByPrimaryKey(string primaryKeyValue, SqlTransaction tran = null)
        {
            T entity = default(T);
            if (string.IsNullOrEmpty(_primaryKey))
                return entity;
            string sql = string.Format("Select * From dbo.{0} Where ({1} = @PrimaryKey)", _tableName, _primaryKey);
            SqlParameter param = new SqlParameter("@PrimaryKey", primaryKeyValue);
            if (tran == null)
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(this._sqlCon, CommandType.Text, sql, param))
                {
                    if (dr.Read())
                    {
                        entity = DataReaderToEntity(dr);
                    }
                }
            }
            else
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, param);
                if (dr.Read())
                {
                    entity = DataReaderToEntity(dr);
                }
            }
            return entity;
        }

        /// <summary>
        /// 用条件查询对象，只返第一条
        /// <summary>
        /// <param name="jsonPara">条件参数集合</param>
        /// <param name="tran">事务</param>
        /// <returns>指定的对象</returns>
        public T FindByCondition(string condition, SqlTransaction tran = null)
        {
            T entity = default(T);
            string sql = string.Format("Select top 1 * From dbo.{0} {1}", _tableName, condition);
            if (tran == null)
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(this._sqlCon, CommandType.Text, sql, null))
                {
                    if (dr.Read())
                    {
                        entity = DataReaderToEntity(dr);
                    }
                }
            }
            else
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null);
                if (dr.Read())
                {
                    entity = DataReaderToEntity(dr);
                }
            }
            return entity;
        }

        /// <summary>
        /// 返回所有对象
        /// <summary>
        /// <param name="tran">事务</param>
        /// <returns>对象集合</returns>
        public List<T> List(SqlTransaction tran = null)
        {
            T entity = default(T);
            List<T> list = new List<T>();
            string sql = string.Format("Select * From dbo.{0}  Order by {1} {2}", _tableName, _sortField, _isDesc ? "DESC" : "ASC");
            if (tran == null)
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(this._sqlCon, CommandType.Text, sql, null))
                {
                    while (dr.Read())
                    {
                        entity = DataReaderToEntity(dr);
                        list.Add(entity);
                    }
                }
            }
            else
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null);
                while (dr.Read())
                {
                    entity = DataReaderToEntity(dr);
                    list.Add(entity);
                }
            }
            return list;
        }

        public List<T> List(string condition, SqlTransaction tran = null)
        {
            T entity = default(T);
            List<T> list = new List<T>();
            string sql = string.Format("Select * From dbo.{0} {1}  Order by {2} ", _tableName, condition, _sortField);
            if (tran == null)
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(this._sqlCon, CommandType.Text, sql, null))
                {
                    while (dr.Read())
                    {
                        entity = DataReaderToEntity(dr);
                        list.Add(entity);
                    }
                }
            }
            else
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null);
                while (dr.Read())
                {
                    entity = DataReaderToEntity(dr);
                    list.Add(entity);
                }
            }
            return list;
        }

        public List<T> List(string condition, PageInfo page)
        {
            if (!string.IsNullOrEmpty(this._sortField))
            {
                this._sortField = this._sortField.ToLower();
                this._sortField = this._sortField.Replace("desc", "");
            }

            var sp = "proc_Pagination";
            var paras = new[] {   
				new SqlParameter("@tableName",this._tableName),  
				new SqlParameter("@fields","*"),  
				new SqlParameter("@orderField",this._sortField),
				new SqlParameter("@orderType",this._isDesc?1:0),
				new SqlParameter("@sqlWhere",condition),
				new SqlParameter("@pageIndex",page.PageIndex),
				new SqlParameter("@pageSize",page.PageSize),
				new SqlParameter(){ParameterName = "@totalRows",SqlDbType = SqlDbType.Int,Size = 4,Direction = ParameterDirection.Output},
				new SqlParameter(){ParameterName = "@totalPages",SqlDbType = SqlDbType.Int,Size = 4,Direction = ParameterDirection.Output}
			};
            var tmpJObject = JObject.Parse("{}");

            DataTable dt = SqlHelper.ExecuteDataset(this._sqlCon, CommandType.StoredProcedure, sp, paras).Tables[0];
            var list = new List<T>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var item = dr.ToModelInfoOf<T>();
                    list.Add(item);
                }
            }
            page.TotalPages = int.Parse(paras[paras.Length - 1].Value.ToString());
            page.TotalRows = int.Parse(paras[paras.Length - 2].Value.ToString());
            return list;
        }


        #endregion

        #region 通用
        /// <summary>
        /// 查询记录是否存在
        /// </summary>
        /// <param name="jsonPara">条件集合</param>
        /// <param name="tran">事务</param>
        /// <returns>是否存在</returns>
        public bool Exist(string jsonPara, SqlTransaction tran = null)
        {
            var jo = JObject.Parse(jsonPara);
            string[] fields = jo.Properties().Select(item => item.Name.ToString()).ToArray();
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            var conditionArr = new List<string>();
            SqlParameter[] param = new SqlParameter[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                conditionArr.Add(string.Format("{0} = @{0}", fields[i]));
                param[i] = new SqlParameter("@" + fields[i], values[i]);
            }
            var sql = string.Format("Select count(*) From dbo.{0} Where {1} ", _tableName, string.Join(",", conditionArr));
            if (tran == null)
                return int.Parse(SqlHelper.ExecuteScalar(this._sqlCon, CommandType.Text, sql, param).ToString()) > 0;
            else
                return int.Parse(SqlHelper.ExecuteScalar(tran, CommandType.Text, sql, param).ToString()) > 0;

        }
        /// <summary>
        /// 执行存储过程，存储过程最后一个参数必须是@msg
        /// </summary>
        /// <param name="jsonPara">json参数也是存储过程的参数</param>
        /// <param name="spName">存储过程名字</param>
        /// <param name="tran">事务</param>
        /// <returns>是否执行成功</returns>
        public bool ExecuteNonQuery(string jsonPara, string spName, SqlTransaction tran = null)
        {
            var result = false;
            var jo = JObject.Parse(jsonPara);
            string[] fields = jo.Properties().Select(item => item.Name.ToString()).ToArray();
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            SqlParameter[] param = new SqlParameter[fields.Length + 2];
            int i = 0;
            while (i < fields.Length + 1)
            {
                param[i] = new SqlParameter("@" + fields[i], values[i]);
                i++;
            }
            param[i + 1] = new SqlParameter("@msg", null) { Direction = ParameterDirection.Output, Size = 200, SqlDbType = SqlDbType.VarChar };
            if (tran == null)
                result = SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, spName, param) > 0;
            else
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, spName, param) > 0;
            return result;

        }
        /// <summary>
        /// 生成数据事务
        /// </summary>
        /// <param name="conn">数据链接串</param>
        /// <returns></returns>
        public SqlTransaction CreateTransaction(string conn = null)
        {
            var connStr = conn == null ? _sqlCon : conn;
            var sqlConn = new SqlConnection(connStr);
            if (sqlConn.State != ConnectionState.Open)
            {
                sqlConn.Open();
            }
            var trans = sqlConn.BeginTransaction();
            if (trans == null)
            {
                throw new ArgumentNullException("connection");
            }
            return trans;
        }
        #endregion

        #region 其他
        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// (提供了默认的反射机制获取信息，为了提高性能，建议重写该函数)
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected virtual T DataReaderToEntity(SqlDataReader dr)
        {
            T obj = new T();
            PropertyInfo[] pis = obj.GetType().GetProperties();

            foreach (PropertyInfo pi in pis)
            {
                try
                {
                    pi.SetValue(obj, dr[pi.Name] ?? "", null);
                }
                catch { }
            }
            return obj;
        }
        #endregion

        /// <summary>
        /// 获取Model，为以后使用webcache做预留接口
        /// </summary>
        /// <param name="jsonPara"></param>
        /// <returns></returns>
        public virtual T GetModel(string jsonPara)
        {
            return null;
        }


        /// <summary>
        /// 获取model的List，为以后使用webcache做预留接口
        /// </summary>
        /// <param name="jsonPara"></param>
        /// <returns></returns>
        public virtual List<T> GetModelList(string jsonPara)
        {
            return null;
        }

        /// <summary>
        /// 全部清空cache，让所有数据cache失效
        /// </summary>
        public virtual void ClearCache()
        {

        }

        /// <summary>
        /// 根据条件让部分cache失效
        /// </summary>
        /// <param name="jsonPara"></param>
        public virtual void ClearCache(string jsonPara)
        {

        }

        protected void AddToCache(T iModelInfo)
        {
            List<T> cache = cc.common.Cache.CacheMng<T>.GetCache(this._tableName);

            if (cache == null)
            {
                //cache = new List<VProductInfo>();
                cc.common.Cache.CacheMng<T>.AddCache(this._tableName, new List<T>());
                cache = cc.common.Cache.CacheMng<T>.GetCache(this._tableName);
            }

            cache.Add(iModelInfo);
        }
    }

    public class PageInfo
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
    }

}
