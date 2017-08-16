using cc.common.DataConvert;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.PayMng
{
    public class PayBeforeOrderInfo
    {
        public string OrderCodes;
        public decimal Total;
        public string RN;

        public PayBeforeOrderInfo(string iRN, string iOrderCodes, decimal iTotal)
        {
            this.RN = iRN;
            this.OrderCodes = iOrderCodes;
            this.Total = iTotal;
        }
    }

    public class PayMng
    {
        public PayBeforeOrderInfo GetOrderInfoByPayBefore(common.UserInfo iLoginUser, string iRN, string iOrderCodes, int iPayType, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var sp = "[proc_Pay_GetOrderInfoPayBefore]";
            var paras = new[]
                    {
                        new SqlParameter("@rn", iRN),
                        new SqlParameter("@orderCodes", iOrderCodes),
                        new SqlParameter("@sysUserID", iLoginUser.UserId), //权限控制 及参数列表
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}
                    };
            DataSet ds = SqlHelper.ExecuteDataset(common.Sys.SystemConnections.B2bConn, CommandType.StoredProcedure, sp, paras);
            var msg = paras[paras.Length - 1].Value;
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return null;
            }


            if (ds != null && ds.Tables.Count > 1)
            {
                //订单信息
                DataTable dt1 = ds.Tables[0];
                //支付方式信息
                DataTable dt2 = ds.Tables[1];

                #region 判断支付方式是否允许

                //if (dt2 == null || dt2.Rows.Count == 0)
                //{
                //    iErrorMsg = "卖家未配置支付方式";
                //    return null;
                //}

                //var existPayType = (from t in dt2.AsEnumerable()
                //                    where t.Field<int>("PayTypeId") == iPayType
                //                    select t).Count() > 0 ? true : false;

                //if (existPayType == false)
                //{
                //    iErrorMsg = "无法支付，不被卖家认可的支付方式";
                //    return null;
                //}
                #endregion

                #region 获取订单信息，并判断是否能够支付
                if (dt1 == null && dt1.Rows.Count == 0)
                {
                    iErrorMsg = "未获取到要支付的订单信息";
                    return null;
                }

                int canPayCount = (from t in dt1.AsEnumerable()
                                   where t.Field<int>("OrderStatus") == 1
                                   select t).Count();

                decimal total = (from t in dt1.AsEnumerable()
                                 where t.Field<int>("OrderStatus") == 1
                                 select t.Field<decimal>("TotalPrice")).Sum();

                string orderCodes = string.Join(",", (from t in dt1.AsEnumerable()
                                                      where t.Field<int>("OrderStatus") == 1
                                                      select t.Field<string>("OrderCode")).ToArray());



                if (canPayCount <= 0)
                {
                    iErrorMsg = "只有待付款的订单才能够支付";
                    return null;
                }

                if (total <= 0)
                {
                    iErrorMsg = "支付金额错误，无法支付";
                    return null;
                }

                return new PayBeforeOrderInfo(iRN, orderCodes, total);
                #endregion


            }
            else
            {
                iErrorMsg = "查询支付订单信息时出错";
                return null;
            }
        }

        /// <summary>
        /// 获取卖家的支付方式配置
        /// </summary>
        /// <param name="iLoginUser"></param>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public JArray GetSupplierPayType(cc.common.UserInfo iLoginUser, out string iErrorMsg)
        {
            cc.dal.VSupplierPayType spt = new dal.VSupplierPayType();
            return spt.GetSupplierPayType(iLoginUser, out iErrorMsg);
        }

        /// <summary>
        /// 卖家设置支付方式
        /// </summary>
        /// <param name="iLoginUser"></param>
        /// <param name="iPayTypeId"></param>
        /// <param name="iIsOpen"></param>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public bool SetSupplierPayType(common.UserInfo iLoginUser, int iPayTypeId, int iIsOpen, out string iErrorMsg)
        {
            cc.dal.VSupplierPayType spt = new dal.VSupplierPayType();
            return spt.SetSupplierPayType(iLoginUser, iPayTypeId, iIsOpen, out iErrorMsg);
        }


    }
}
