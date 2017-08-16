using cc.basemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.model
{
    public class VStoreOutInfo : BaseModel
    {
        public string OrderCode { get; set; }
        public string Express { get; set; }
        public int ExpId { get; set; }
        public string ExpDesc { get; set; }
        public string UserSN_S { get; set; }
        public string UserSN_R { get; set; }
        public string StoreCode { get; set; }
        public string ExpCode { get; set; }
        public int StoreOutNum { get; set; }
        public int ExpressId { get; set; }
        public int SmsRemindTimes { get; set; }
        public int Status { get; set; }
        public DateTime ConfirmDateTime { get; set; }
        public DateTime ExpDateTime { get; set; }


        /// <summary>
        /// 扩展字段，出库单详情信息
        /// </summary>
        public List<VStoreOutDetailInfo> DetailList;
    }

    public enum StoreOutStatus
    {
        /// <summary>
        /// 已出库
        /// </summary>
        StoreOut = 1,
        /// <summary>
        /// 已发货
        /// </summary>
        Deliver = 2,
    }
}
