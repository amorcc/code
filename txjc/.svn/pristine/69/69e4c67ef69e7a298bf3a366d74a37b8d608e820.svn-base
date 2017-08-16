using cc.unit.WeChat.Pay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cc.webapi.WeChat
{
    public partial class wx_pay_notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cc.log.Log.Debug(this.GetType(), null, string.Format("调用了支付回调页面11"));
                ResultNotify resultNotify = new ResultNotify(this);
                resultNotify.ProcessNotify();
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(this.GetType(), ex);
            }
        }
    }
}