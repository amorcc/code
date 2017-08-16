using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.common
{
    public class UserInfo
    {
        public string Token;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId;
        /// <summary>
        /// 用户添加时间
        /// </summary>
        public DateTime UserDateAdded;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName;
        /// <summary>
        /// 用户联系电话
        /// </summary>
        public string Phone;
        /// <summary>
        /// Email
        /// </summary>
        public string Email;

        public string UserSN;
        public string RealName;
        public int RegisterSource;
        public int IsAdmin;
        public string Tier;

        /// <summary>
        /// 是否存在公司信息
        /// </summary>
        public bool IsExistCompanyInfo;

        public DateTime CompanyDateAdded;

        public string CompanyName;
        public int AreaCode;
        public string CompanyPhone;
        public string CompanyAddress;
        /// <summary>
        /// 主营业务
        /// </summary>
        public string BusinessScope;

        double lastVisitTimeStamp = 0;

        /// <summary>
        /// 是否开通卖家服务
        /// </summary>
        public int IsOpenSupplier = 0;

        public string Openid = "";

        public UserInfo()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.lastVisitTimeStamp = ts.TotalMilliseconds;
        }

        /// <summary>
        /// 更新最后访问时间
        /// </summary>
        public void UpdateVisitTime()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.lastVisitTimeStamp = ts.TotalMilliseconds;
        }

        /// <summary>
        /// 验证用户是否已经过期
        /// cc  2016-11-19
        /// 40分钟不操作，就算过期
        /// </summary>
        /// <returns></returns>
        public bool IsTimeOut()
        {
            TimeSpan tsNow = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            double ts = tsNow.TotalMilliseconds - this.lastVisitTimeStamp;

            string sessionTimeMinStr = cc.common.Sys.AppMng.App("SessionTimeMin");

            int sessionTimeMin = 40;

            int.TryParse(sessionTimeMinStr, out sessionTimeMin);

            if (ts > sessionTimeMin * 60 * 1000)
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
