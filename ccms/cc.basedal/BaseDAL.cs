using cc.basemodel;
using cc.utility;
using cc.utility.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace cc.basedal
{
    public class BaseDAL<T> where T : BaseModel, new()
    {
        protected string _sqlCon = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;//数据连接
        protected string _tableName;//需要初始化的对象表名
        protected string _primaryKey = "Id";//数据库的主键字段名
        protected string _sortField = "Id Desc";//排序字段

        public BaseDAL()
        {
            if (string.IsNullOrEmpty(_tableName))
            {
                #region 反射获取表名
                Type t = typeof(T);

                //cc.utility.Data.TableAttribute tableAttr 
                object[] objs = t.GetCustomAttributes(typeof(cc.utility.Data.TableAttribute), false);

                if (objs != null && objs.Count() > 0 && objs[0] is cc.utility.Data.TableAttribute)
                {
                    this._tableName = (objs[0] as cc.utility.Data.TableAttribute).TableName;
                }
                #endregion
            }
        }

        /// <summary>
        /// 是否是数据库字段
        /// </summary>
        /// <param name="iColAttr"></param>
        /// <returns></returns>
        bool IsTableColumn(PropertyInfo iPI)
        {
            ColumnAttribute colAttr = (iPI.GetCustomAttribute(typeof(ColumnAttribute), false) as ColumnAttribute);
            if (colAttr != null && colAttr.IsTableColumn == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region 新增
        public bool Insert(T obj, SqlTransaction tran = null)
        {
            var result = false;
            Type t = obj.GetType();
            PropertyInfo[] pis = t.GetProperties();
            var parmArr = new List<string>();
            var fieldArr = new List<string>();
            var lstSqlParameter = new List<SqlParameter>();
            foreach (PropertyInfo pi in pis)
            {
                //非数据库字段直接跳出
                if (this.IsTableColumn(pi) == false)
                {
                    continue;
                }

                //主键直接跳出
                if (pi.Name.ToLower() == "id")
                    continue;
                PropertyInfo pInfo = t.GetProperty(pi.Name);
                var value = pInfo.GetValue(obj, null);
                if (value == null)
                    continue;
                if (value.GetType() == typeof(JArray))
                {
                    value = value.ToString();
                }
                parmArr.Add(string.Format("@{0}", pi.Name));
                fieldArr.Add(string.Format("{0}", pi.Name));
                lstSqlParameter.Add(new SqlParameter("@" + pi.Name, value));
            }

            var sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", this._tableName, string.Join(",", fieldArr), string.Join(",", parmArr));
            if (tran == null)
                result = SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.Text, sql, lstSqlParameter.ToArray()) > 0;
            else
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, lstSqlParameter.ToArray()) > 0;
            return result;
        }
        #endregion

        #region 修改
        public bool Update(T obj, SqlTransaction tran = null)
        {
            var result = false;
            Type t = obj.GetType();
            PropertyInfo[] pis = t.GetProperties();
            //获取本次更新的主键值
            var primaryValue = t.GetProperty(_primaryKey);
            if (primaryValue == null)
                throw new MyException("缺少主键参数");
            var parmStr = new List<string>();
            var lstSqlParameter = new List<SqlParameter>();
            foreach (PropertyInfo pi in pis)
            {
                //非数据库字段直接跳出
                if (this.IsTableColumn(pi) == false)
                {
                    continue;
                }

                //主键直接跳出
                if (pi.Name.ToLower() == this._primaryKey.ToLower() || pi.Name.ToLower() == "id")
                    continue;
                PropertyInfo pInfo = t.GetProperty(pi.Name);
                var value = pInfo.GetValue(obj, null);
                if (value == null)
                    continue;
                if (value.GetType() == typeof(JArray))
                {
                    value = value.ToString();
                }
                parmStr.Add(string.Format("{0} = @{0}", pi.Name));
                lstSqlParameter.Add(new SqlParameter("@" + pi.Name, value));
            }
            string sql = string.Format("UPDATE {0} SET {1} WHERE {2} = '{3}' ", this._tableName, string.Join(",", parmStr), _primaryKey, primaryValue.GetValue(obj, null));
            if (tran == null)
                result = SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.Text, sql, lstSqlParameter.ToArray()) > 0;
            else
                result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, lstSqlParameter.ToArray()) > 0;
            return result;
        }

        #endregion

        #region 查询
        #region List

        public T FindByCondition(string condition, SqlTransaction tran = null)
        {
            T entity = default(T);
            string sql = string.Format("Select top 1 * From dbo.{0} {1} {2}", _tableName, condition, string.IsNullOrEmpty(_sortField) ? "" : "Order by " + _sortField);
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
                dr.Close();
            }
            return entity;
        }

        /// <summary>
        /// 按条件查询所有记录
        /// </summary>
        /// <param name="jsonPara">条件</param>
        /// <param name="tran">事务</param>
        /// <returns>对象集合</returns>
        public List<T> List(string condition, SqlTransaction tran = null)
        {
            T entity = default(T);
            List<T> list = new List<T>();
            string sql = string.Format("Select * From dbo.{0} {1}  {2} ", _tableName, condition, string.IsNullOrEmpty(_sortField) ? "" : "Order by " + _sortField);
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
                dr.Close();
            }
            return list;
        }

        #endregion
        #endregion
        #region 根据主键查询实体
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
                dr.Close();
            }
            return entity;
        }
        #endregion

        #region DataReader to model
        protected virtual T DataReaderToEntity(SqlDataReader dr)
        {
            T obj = new T();
            PropertyInfo[] pis = obj.GetType().GetProperties();

            foreach (PropertyInfo pi in pis)
            {
                try
                {
                    //if (dr[pi.Name].ToString() != "")
                    //{
                    pi.SetValue(obj, dr[pi.Name] ?? "", null);
                    //}
                }
                catch { }
            }
            return obj;
        }
        #endregion

        public DataSet ExecuteDataset(string iSPName)
        {
            Console.WriteLine("BaseDAL<T>:" + iSPName);
            return null;
        }

        public T GetModel(T para)
        {
            Console.WriteLine("BaseDAL<T>:getmodel");
            return para;
        }
    }
}
