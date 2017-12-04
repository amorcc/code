using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.ComponentModel;

namespace Common
{
    // A base class that implements IDisposable.
    // By implementing IDisposable, you are announcing that
    // instances of this type allocate scarce resources.
    public class MyDisposable : IDisposable
    {
        // Track whether Dispose has been called.
        private bool disposed = false;

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    DisposeManaged();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                DisposeUnManaged();

                // Note disposing has been done.
                disposed = true;
            }
        }

        protected virtual void DisposeManaged()
        {
        }

        protected virtual void DisposeUnManaged()
        {
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~MyDisposable()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        ///// <summary>
        ///// 得到web.config里配置项的数据库连接字符串。
        ///// </summary>
        ///// <param name="configName"></param>
        ///// <returns></returns>
        //public static string GetConnectionString(string configName)
        //{
        //    string connectionString = ConfigurationManager.AppSettings[configName];
        //    string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
        //    if (ConStringEncrypt == "true")
        //    {
        //        connectionString = EjiangLib.Encrypt.DESEncrypt.Decrypt(connectionString);
        //    }

        //    return connectionString;
        //}

        ///// <summary>
        /////  得到web.config里配置项的数据库连接字符串。
        ///// </summary>
        ///// <param name="configName"></param>
        ///// <returns></returns>
        //public static string GetConnectionStringFromConntionSetting(string configName)
        //{
        //    if (ConfigurationManager.ConnectionStrings[configName] == null)
        //    {
        //        return string.Empty;
        //    }

        //    string conn = ConfigurationManager.ConnectionStrings[configName].ToString();
        //    if (conn == string.Empty)
        //    {
        //        return string.Empty;
        //    }

        //    if (conn.IndexOf(";") <= 0)
        //    {
        //        return EjiangLib.Encrypt.DESEncrypt.Decrypt(conn);
        //    }

        //    return conn;
        //}
    }
}