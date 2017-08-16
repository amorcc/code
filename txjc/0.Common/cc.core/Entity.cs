using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace cc.core
{
    /// <summary>
    /// 实体基类，所有实体类都应该集成它
    /// </summary>
    [Serializable]
    public abstract class MyEntity : IMyEntity
    {
        protected MyEntity()
        {
            RootId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 实体主键，方法查询，删除，更新等操作
        /// </summary>
        public string RootId { get; private set; }
    }


    /// <summary>
    /// 系统内部服务的基础类
    /// </summary>
    public abstract class MyService:IMyService
    {
        /// <summary>
        /// 实例方法从数据库里读取的缓存数据
        /// </summary>
        public DataSet CachedData { get; protected set; }
        /// <summary>
        /// 服务基于哪个数据连接
        /// </summary>
        public string DbConnection { get; private set; }

        protected MyService(string strCon)
        {
            DbConnection = strCon;            
        }
        #region 共有函数
        protected abstract void ResetCachedData(string jsonPara);
        #endregion

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
        ~MyService()
        {
            this.Dispose(false);
        }
        #endregion
        
        /// <summary>
        /// Provides the facility that disposes the object in an explicit manner,
        /// preventing the Finalizer from being called after the object has been
        /// disposed explicitly.
        /// </summary>
        private void ExplicitDispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    public abstract class MyEntityComparer : IEqualityComparer<IMyEntity>
    {
        public abstract bool Equals(IMyEntity myEntity1, IMyEntity myEntity2);

        public abstract int GetHashCode(IMyEntity myEntity);

    }



}
