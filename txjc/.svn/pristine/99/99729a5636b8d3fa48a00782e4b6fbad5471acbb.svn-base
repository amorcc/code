using cc.basedal;
using cc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace cc.dal
{
    /// <summary>
    /// 商品
    /// </summary>
    public class ProductStateFlow : BaseDAL<ProductStateFlowInfo>
    {
        public ProductStateFlow()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "Product_StateFlow";
            this._sortField = "Id";
            this._isDesc = true;
        }

        public bool Insert(int iUserId, int iStatus, string iShortDesc, string iDescription)
        {
            JObject para = new JObject()
            {
                {"UserAdd",iUserId},
                {"Status",iStatus},
                {"ShortDesc",iShortDesc},
                {"Description",iDescription},
            };

            return base.Insert(para.ToString());
        }
    }
}
