using cc.common.Utility;
using cc.unit.ProductOrder.Preview;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.ProductOrder.Create
{
    public class OrderCreate
    {

        /// <summary>
        /// 买家UserSN
        /// </summary>
        string mUserSN_R;

        /// <summary>
        /// 买家RoleId
        /// </summary>
        int mRoleId;

        /// <summary>
        /// 商品信息： 
        /// </summary>
        string mProInfo;

        /// <summary>
        /// 订单来源
        /// </summary>
        OrderSource mOrderSource;

        /// <summary>
        /// RN
        /// </summary>
        string mRN;

        /// <summary>
        /// 收货地址
        /// </summary>
        int mAddressId;

        /// <summary>
        /// 剩余金额支付方式：7-线上，6-线下
        /// </summary>
        int mPrimaryPayType;

        /// <summary>
        /// 订单来源：200为B2B PC下单
        /// </summary>
        int mPartnerId = 200;

        List<OrderCreateInfo> mOCInfoList = new List<OrderCreateInfo>();

        common.UserInfo mLoginUser;

        Preview.Preview mPreview = null;
        JObject mPreviewResult;

        /// <summary>
        /// 订单总金额，给微信发消息时用
        /// </summary>
        decimal mSumOfTotal = 0;

        /// <summary>
        /// 所有ordercode
        /// </summary>
        List<string> mOrderCodes = new List<string>();

        /// <summary>
        /// 发送给卖家微信的信息
        /// </summary>
        List<SendOrderCreateInfo> mSendToWxInfo = new List<SendOrderCreateInfo>();

        public OrderCreate(common.UserInfo iLoginUser, string iProInfoStr, string iBatchId, Preview.OrderSource iOrderSource, int iAddressId, JArray iOrderCreateInfo, int iPartnerId = 200)
        {
            this.mLoginUser = iLoginUser;
            this.mProInfo = iProInfoStr;
            this.mRN = iBatchId;
            this.mOrderSource = iOrderSource;
            this.mAddressId = iAddressId;
            this.mPartnerId = iPartnerId;

            if (iOrderCreateInfo != null && iOrderCreateInfo.Count > 0)
            {
                foreach (JObject item in iOrderCreateInfo)
                {
                    string userSN_S = item.GetValueExt<string>("SupplierUserSN");
                    string message = item.GetValueExt<string>("Message");

                    this.mOCInfoList.Add(new OrderCreateInfo(userSN_S, message));
                }
            }
        }

        public JObject Create(out string iErrorMsg)
        {
            JObject result = new JObject();
            iErrorMsg = string.Empty;

            #region 运行一遍订单预览，看是否有问题，如果有问题直接退出
            this.mPreview = new Preview.Preview(this.mLoginUser, this.mProInfo, this.mOrderSource, this.mRN);
            this.mPreviewResult = this.mPreview.GetPreviewInfo(out iErrorMsg);

            if (!string.IsNullOrEmpty(iErrorMsg) || this.mPreviewResult == null || this.mPreviewResult.GetValueExt<int>("ResponseID") != 0)
            {
                iErrorMsg = string.IsNullOrEmpty(iErrorMsg) ? "生成订单出错" : iErrorMsg;
                return result;
            }
            #endregion

            var transName = "myOrder";
            var sqlconn = new SqlConnection(cc.common.Sys.SystemConnections.B2bConn);
            if (sqlconn.State != ConnectionState.Open)
            {
                sqlconn.Open();
            }

            var trans = sqlconn.BeginTransaction(transName);

            try
            {
                if (this.mPreview.mSupplierInfoList != null && this.mPreview.mSupplierInfoList.Count > 0)
                {
                    foreach (var supplier in this.mPreview.mSupplierInfoList)
                    {
                        #region 循环所有卖家，逐个生成订单
                        //生成订单号
                        var orderCode = cc.common.Utility.SerialNumber.CreateOrderCode();

                        var proInfoList = (from t in this.mPreview.mProInfo
                                           where t.ProInfo.UserSN == supplier.UserSN
                                           select t).ToList();

                        OrderCreateInfo oci = (from t in this.mOCInfoList
                                               where t.UserSN_S == supplier.UserSN
                                               select t).FirstOrDefault();

                        this.mOrderCodes.Add(orderCode);

                        #region 写入订单详情信息
                        foreach (var pro in proInfoList)
                        {
                            int index = proInfoList.IndexOf(pro) + 1;
                            var subOrderCode = "S" + orderCode + index.ToString().PadLeft(2, '0');
                            iErrorMsg += this.DetailInsert(trans, orderCode, subOrderCode, pro.ProId, pro.ProInfo.Name, pro.ProInfo.Image, pro.ProCount, pro.ProInfo.Price, pro.SubTotal, pro.TransFee);

                            if (!string.IsNullOrEmpty(iErrorMsg))
                            {
                                return result;
                            }
                        }
                        #endregion

                        //不含运费的订单小计
                        decimal sumOfSubTotal = (from t in proInfoList
                                                 where t.ProInfo.UserSN == supplier.UserSN
                                                 select t.SubTotal).Sum();

                        //订单的运费小计
                        decimal sumOfTransFee = (from t in proInfoList
                                                 where t.ProInfo.UserSN == supplier.UserSN
                                                 select t.TransFee).Sum();

                        //订单的合计
                        decimal sumOfTotal = sumOfSubTotal + sumOfTransFee;

                        this.mSumOfTotal += sumOfSubTotal;

                        this.mSendToWxInfo.Add(new SendOrderCreateInfo(supplier.UserSN, supplier.Openid, orderCode, this.mLoginUser.CompanyName, sumOfSubTotal));

                        #region 开始写主订单信息
                        iErrorMsg += this.OrderInsert(trans, orderCode, sumOfTransFee, sumOfSubTotal, sumOfSubTotal, oci.Message, "", this.mAddressId, this.mRN, 1, this.mLoginUser.UserSN, this.mLoginUser.CompanyName, supplier.UserSN, supplier.CompanyName, this.mLoginUser.UserId, this.mPartnerId);
                        #endregion

                        #region 开始下单减库存
                        //改到写订单详情的时候，修改库存
                        #endregion

                        #region 删除购物车
                        var proIdList = (from t in proInfoList
                                         where t.ProInfo.UserSN == supplier.UserSN
                                         select t.ProId).ToList();
                        cc.dal.Cart cartDal = new dal.Cart();
                        iErrorMsg += cartDal.RemoveCart(this.mLoginUser.UserSN, proIdList, trans);
                        #endregion

                        #endregion
                    }



                    if (!string.IsNullOrEmpty(iErrorMsg))
                    {
                        trans.Rollback(transName);
                        return result;
                    }
                    else
                    {
                        trans.Commit();

                        #region 给微信发消息
                        WeiXin.WeiXin weixin = new WeiXin.WeiXin();
                        string access_token = weixin.GetAccessToken();

                        //给买家发
                        //weixin.SendOrderCreateMsgToRetailerWx(access_token, this.mLoginUser.UserName, this.mLoginUser.Openid, string.Join(",", this.mOrderCodes.ToArray()), this.mSumOfTotal, "如果您选择线下支付，请上传线下支付凭证，卖家审核后发货");

                        //给卖家发
                        weixin.SendOrderCreateMsgToSupplierWx(this.mSendToWxInfo, access_token);
                        #endregion


                        result.SetProperty("OrderCode", string.Join(",", this.mOrderCodes.ToArray()));

                        return result;
                    }
                }
                else
                {
                    iErrorMsg = "没有查询到该订单的卖家信息！";
                    return result;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback(transName);
                iErrorMsg = ex.ToString();
                cc.log.Log.Error(typeof(OrderCreate), ex);
                return new JObject();
            }
            finally
            {
                trans.Dispose();
                sqlconn.Close();
            }

        }

        #region 订单细节
        /// <summary>
        /// 订单细节写入
        /// </summary>
        /// <param name="tran">数据库事务</param>
        /// <param name="orderCode">订单号</param>
        /// <param name="subOrderCode">子订单号</param>
        /// <param name="proId">产品编码</param>
        /// <param name="proName">产品名称</param>
        /// <param name="proImage">产品图片</param>
        /// <param name="receiptProvided">是否提供发票</param>
        /// <param name="rate">费率</param>
        /// <param name="proCount">购买数量</param>
        /// <param name="proPrice">公开价格</param>
        /// <param name="proPrice1">购买价格:成交价格</param>
        /// <param name="tranFee">该产品运费</param>
        /// <param name="cod">该产品是否到付2016-11-07</param>
        /// <param name="activityId">活动编码</param>
        /// <param name="subsidy">活动补贴，如直降活动平台补贴100（subsidy_100_0=100）</param>
        /// <returns></returns>
        private string DetailInsert(SqlTransaction tran, string orderCode, string subOrderCode, int proId, string proName, string proImage, int proCount, decimal proPrice, decimal subTotal, decimal tranFee)
        {
            var sp = "proc_ProductOrder_Create_OrderDetail";
            var paras = new[]
                    {
                        new SqlParameter("@orderCode", orderCode),
                        new SqlParameter("@subOrderCode", subOrderCode),
                        new SqlParameter("@proId", proId),
                        new SqlParameter("@proName", proName),
                        new SqlParameter("@proImage", proImage),
                        new SqlParameter("@proCount", proCount),
                        new SqlParameter("@proPrice", proPrice),
                        new SqlParameter("@transFee", tranFee),
                        new SqlParameter("@subTotal", subTotal),
                        new SqlParameter("@sysUserID", this.mLoginUser.UserId),
                        new SqlParameter("@msg",null)
                        {Direction = ParameterDirection.Output,Size = 200,SqlDbType = SqlDbType.VarChar}
                    };
            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, sp, paras);
            var msg = paras[paras.Length - 1].Value;
            //my.log.MySystemLog.OrderLog(typeof(OrderService), "", orderCode, log.BusinessCode.子订单生成, msg, "子订单生成", string.Format("rn='{0}',订单号='{1}',子订单号='{2}',。", rn, orderCode, subOrderCode), "", "", "", rn);

            if (msg == null || string.IsNullOrEmpty(msg.ToString()))
            {
                string errorMsg = string.Format("rn='{0}',订单号='{1}',子订单号='{2}',。", this.mRN, orderCode, subOrderCode);
            }

            return msg.ToString();
        }

        #endregion

        #region 主订单写入
        /// <summary>
        /// 订单表写入
        /// </summary>
        /// <param name="tran">数据事务</param>
        /// <param name="orderCode">订单号</param>
        /// <param name="tranFee">总运费</param>
        /// <param name="totalPrice">总价（df:下单时用户成交总价）</param>
        /// <param name="finalPrice">成交价（df：改价后的总价）</param>
        /// <param name="orderMsg">订单留言</param>
        /// <param name="needBill">是否要发票</param>
        /// <param name="billTitle">发票抬头</param>
        /// <param name="express">物流方式</param>
        /// <param name="addressId">收货地址</param>
        /// <param name="batchId">订单预览编号</param>
        /// <param name="primaryPayType">主要支付方式</param>
        /// <param name="billKind">发票类型</param>
        /// <param name="taxNum">纳税人识别码</param>
        /// <param name="bankName">开户行</param>
        /// <param name="bankAccount">银行帐号</param>
        /// <param name="regAddress">注册地址</param>
        /// <param name="regTel">注册电话</param>
        /// <param name="taxImage">税务登记证</param>
        /// <param name="yiBanImage">一般纳税人证书图片</param>
        /// <param name="doc1">开票资料（盖章）</param>
        /// <param name="userSN_R">买家编码</param>
        /// <param name="retailer">买家公司名称</param>
        /// <param name="userSN_S">卖家编码</param>
        /// <param name="supplier">卖家公司号</param>
        /// <param name="actId">如果大于0表示活动订单</param>
        /// <param name="userId">下单人</param>
        /// <param name="partnerId">订单来源（200 PC端）</param>
        private string OrderInsert(SqlTransaction tran, string orderCode, decimal tranFee, decimal totalPrice, decimal finalPrice, string orderMsg, string express, int addressId, string batchId, int primaryPayType, string userSN_R, string retailer, string userSN_S, string supplier, int userId, int partnerId)
        {
            var sp = "proc_ProductOrder_Create_Order";
            var paras = new[]
                    {
                        new SqlParameter("@orderCode", orderCode),//订单号
                        new SqlParameter("@tranFee", tranFee),
                        new SqlParameter("@totalPrice", totalPrice),
                        new SqlParameter("@finalPrice", finalPrice),
                        new SqlParameter("@orderMsg", orderMsg),
                        new SqlParameter("@express", express),
                        new SqlParameter("@addressId", addressId),
                        new SqlParameter("@batchId", batchId),
                        new SqlParameter("@primaryPayType", primaryPayType),
                        new SqlParameter("@userSN_R", userSN_R),
                        new SqlParameter("@retailer", retailer),
                        new SqlParameter("@userSN_S", userSN_S),
                        new SqlParameter("@supplier", supplier),
                        new SqlParameter("@partnerId", partnerId),//从配置文件读取订单来源
                        new SqlParameter("@sysUserID", userId),
                        new SqlParameter("@msg",null)
                        {Direction = ParameterDirection.Output,Size = 200,SqlDbType = SqlDbType.VarChar}
                    };
            SqlHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, sp, paras);
            string msg = paras[paras.Length - 1].Value.ToString();
            //my.log.MySystemLog.OrderLog(typeof(OrderService), userId.ToString(), orderCode, log.BusinessCode.订单生成, true, "主订单生成", string.Format("rn='{0}',订单号='{1}'。", batchId, orderCode), "", "", "", "");
            return msg;

        }

        #endregion

    }
}
