using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.data.access.sqlServer
{
    public class SqlTransHelper
    {

        private static SqlTransHelper _instance;
        public static SqlTransHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SqlTransHelper();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 在某个数据库连接上启动一个事务
        /// </summary>
        /// <param name="dbConnection">在哪个数据库连接上开启事务,默认空,如果没有,则用当前DBFactory的B2bConnection</param>
        /// <param name="transName">事务的名称</param>
        /// <returns></returns>
        public SqlTransaction BeginTransaction(string transName, string dbConnection)
        {
            var objCon = new SqlConnection(dbConnection);
            try
            {
                if (objCon.State != ConnectionState.Open)
                {
                    objCon.Open();
                }
                var trans = objCon.BeginTransaction(transName);
                return trans;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(@"SqlTransHelper.BeginTransaction fail:{0}", ex.Message));
            }

        }

        /// <summary>
        /// 提交当前事务并释放连接
        /// </summary>
        /// <param name="trans"></param>
        public void CommitTransaction(SqlTransaction trans)
        {
            if (trans == null)
            {
                return;
            }
            try
            {
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(@"SqlTransHelper.CommitTransaction fail:{0}", ex.Message));
            }
            finally
            {
                trans.Dispose();
            }

        }
        /// <summary>
        /// 回滚当前事务并释放连接
        /// </summary>
        /// <param name="trans"></param>
        public void RollbackTransaction(SqlTransaction trans)
        {
            if (trans == null)
            {
                return;
            }
            try
            {
                trans.Rollback();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(@"SqlTransHelper.RollbackTransaction fail:{0}", ex.Message));
            }
            finally
            {
                trans.Dispose();
            }

        }
    }
}
