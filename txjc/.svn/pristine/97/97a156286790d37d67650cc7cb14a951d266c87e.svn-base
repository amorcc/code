using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.basemodel
{
    [Serializable]
    public class BaseModel
    {
        protected int _id;

        protected DateTime _dateAdded;

        protected DateTime _CacheTime;

        public int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public DateTime DateAdded
        {
            get
            {
                return this._dateAdded;
            }
            set
            {
                this._dateAdded = value;
            }
        }

        public DateTime CacheTime
        {
            get
            {
                return this._CacheTime;
            }
            set
            {
                this._CacheTime = value;
            }
        }

        public BaseModel()
        {
            this._dateAdded = DateTime.Now;
            this.CacheTime = DateTime.Now;
        }

        /// <summary>
        /// cache保存时间(单位秒)
        /// -1时，是永不过期
        /// </summary>
        public int _CacheTimeOutSeconds = 20;

        /// <summary>
        /// cache是否到期
        /// </summary>
        /// <returns></returns>
        public bool IsCacheTimeOut()
        {
            if (this._CacheTimeOutSeconds == -1)
            {
                return false;
            }

            DateTime dt = this.CacheTime.AddSeconds(this._CacheTimeOutSeconds);

            if (DateTime.Now > dt)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
