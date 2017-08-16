using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace cc.data.access
{
    public class SqlClient : ISqlClient, IDisposable
    {
        public string DbConnectionString { get; set; }


        /// <summary>
        /// 默认传递一个ConnectionString
        /// </summary>
        /// <param name="dbConnectionString">数据库的连接</param>
        public SqlClient(string dbConnectionString)
        {
            DbConnectionString = dbConnectionString;
        }



        #region Dispose

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {

                }
            }
            this._disposed = true;
        }


        public void Dispose()
        {
            this.ExplicitDispose();
        }

        #endregion

        #region Finalization Constructs
        /// <summary>
        /// Finalizes the object.
        /// </summary>
        ~SqlClient()
        {
            this.Dispose(false);
        }
        #endregion

        /// <summary>
        /// Provides the facility that disposes the object in an explicit manner,
        /// preventing the Finalizer from being called after the object has been
        /// disposed explicitly.
        /// </summary>
        protected void ExplicitDispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
