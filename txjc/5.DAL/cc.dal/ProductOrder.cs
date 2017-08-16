using cc.basedal;
using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.dal
{
    public class ProductOrder : BaseDAL<VProductOrderInfo>
    {
        public ProductOrder()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "v_ProductOrder";
            this._sortField = "DateAdded";
            this._isDesc = true;
        }

        protected override VProductOrderInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new VProductOrderInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.Address = Convert.ToString(dr["Address"] == DBNull.Value ? "" : dr["Address"]);
            obj.Express = Convert.ToString(dr["Express"] == DBNull.Value ? "" : dr["Express"]);
            obj.AreaCode = Convert.ToInt32(dr["AreaCode"] == DBNull.Value ? 0 : dr["AreaCode"]);
            obj.DateModifed = Convert.ToDateTime(dr["DateModifed"] == DBNull.Value ? "1900-01-01 00:00:00" : dr["DateModifed"]);
            obj.Express = Convert.ToString(dr["Express"] == DBNull.Value ? "" : dr["Express"]);
            obj.FinalPrice = Convert.ToDecimal(dr["FinalPrice"] == DBNull.Value ? 0 : dr["FinalPrice"]);
            obj.Message = Convert.ToString(dr["Message"] == DBNull.Value ? "" : dr["Message"]);
            obj.OrderCode = Convert.ToString(dr["OrderCode"] == DBNull.Value ? "" : dr["OrderCode"]);
            obj.OrderStatus = Convert.ToInt32(dr["OrderStatus"] == DBNull.Value ? 0 : dr["OrderStatus"]);
            obj.Phone = Convert.ToString(dr["Phone"] == DBNull.Value ? "" : dr["Phone"]);
            obj.PrimaryPayType = Convert.ToInt32(dr["PrimaryPayType"] == DBNull.Value ? 0 : dr["PrimaryPayType"]);
            obj.Receiver = Convert.ToString(dr["Receiver"] == DBNull.Value ? "" : dr["Receiver"]);
            obj.Retailer = Convert.ToString(dr["Retailer"] == DBNull.Value ? "" : dr["Retailer"]);
            obj.SendNum = Convert.ToInt32(dr["SendNum"] == DBNull.Value ? 0 : dr["SendNum"]);
            obj.StatusReason = Convert.ToString(dr["StatusReason"] == DBNull.Value ? "" : dr["StatusReason"]);
            obj.Supplier = Convert.ToString(dr["Supplier"] == DBNull.Value ? "" : dr["Supplier"]);
            obj.TotalPrice = Convert.ToDecimal(dr["TotalPrice"] == DBNull.Value ? 0 : dr["TotalPrice"]);
            obj.TransFee = Convert.ToDecimal(dr["TransFee"] == DBNull.Value ? 0 : dr["TransFee"]);
            obj.UserID = Convert.ToInt32(dr["UserID"] == DBNull.Value ? 0 : dr["UserID"]);
            obj.UserSN_R = Convert.ToString(dr["UserSN_R"] == DBNull.Value ? "" : dr["UserSN_R"]);
            obj.UserSN_S = Convert.ToString(dr["UserSN_S"] == DBNull.Value ? "" : dr["UserSN_S"]);
            obj.ZipCode = Convert.ToInt32(dr["ZipCode"] == DBNull.Value ? 0 : dr["ZipCode"]);
            obj.BatchId = Convert.ToString(dr["BatchId"] == DBNull.Value ? "" : dr["BatchId"]);
            obj.PayTypeName = Convert.ToString(dr["PayTypeName"] == DBNull.Value ? "" : dr["PayTypeName"]);

            return obj;
        }

        public List<VProductOrderInfo> GetProductOrderList(string iBatchId, string iOrderCodes)
        {
            if (!string.IsNullOrEmpty(iBatchId) && !string.IsNullOrEmpty(iOrderCodes))
            {
                List<string> orderCodeList = iOrderCodes.Split(',').ToList();

                var orderCodeArray = (from t in orderCodeList
                                      select "'" + t + ",").ToArray();

                string orderCodes = string.Join(",", orderCodeArray);

                SearchCondition sc = new SearchCondition();
                sc.AddCondition("BatchId", iBatchId, OperateType.Equal);
                sc.AddCondition("OrderCode", iOrderCodes, OperateType.In);

                return base.List(sc.ConditionStr);

            }
            else
            {
                return new List<VProductOrderInfo>();
            }
        }

        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="iOrderCodes"></param>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public bool OrderPay(string iOrderCodes, int iPayType, out DataSet iDS, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            iDS = null;
            var sp = "proc_Pay_Payment";
            var paras = new[]
                    {
                        new SqlParameter("@orderCodes", iOrderCodes),//订单号
                        new SqlParameter("@payType", iPayType),//订单号
                        new SqlParameter("@msg",null)
                        {Direction = ParameterDirection.Output,Size = 200,SqlDbType = SqlDbType.VarChar}
                    };
            using (DataSet ds = SqlHelper.ExecuteDataset(cc.common.Sys.SystemConnections.B2bConn, CommandType.StoredProcedure, sp, paras))
            {
                var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
                if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                {
                    iErrorMsg = msg.ToString();
                    return false;
                }

                iDS = ds;

                return true;
            }
        }

        #region 买家订单列表

        public List<VProductOrderInfo> GetSellerList(cc.common.UserInfo iLoginUser, string iOrderCode, int iOrderStatus, string iRetailerName, string iStartDate, string iEndDate, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN_S", iLoginUser.UserSN, OperateType.In);

            if (!string.IsNullOrEmpty(iOrderCode))
            {
                sc.AddCondition("OrderCode", iOrderCode, OperateType.Equal);
            }

            if (iOrderStatus == 1)
            {
                sc.AddCondition("OrderStatus", "1,11", OperateType.In);
            }
            else if (iOrderStatus == 2)
            {
                sc.AddCondition("OrderStatus", "2,3,31", OperateType.In);
            }
            else if (iOrderStatus > -2)
            {
                sc.AddCondition("OrderStatus", iOrderStatus.ToString(), OperateType.Equal);
            }

            if (!string.IsNullOrEmpty(iRetailerName))
            {
                sc.AddCondition("Retailer", iRetailerName, OperateType.Like);
            }

            if (!string.IsNullOrEmpty(iStartDate))
            {
                DateTime start;

                if (DateTime.TryParse(iStartDate, out start) == true)
                {
                    sc.AddCondition("DateAdded", start.ToString("yyyy-MM-dd 00:00:00"), OperateType.MoreThan);
                }
            }

            if (!string.IsNullOrEmpty(iEndDate))
            {
                DateTime end;

                if (DateTime.TryParse(iEndDate, out end) == true)
                {
                    sc.AddCondition("DateAdded", end.ToString("yyyy-MM-dd 23:59:59"), OperateType.MoreThan);
                }
            }

            PageInfo pi = new PageInfo();
            pi.PageIndex = iPageIndex;
            pi.PageSize = iPageSize;
            List<VProductOrderInfo> poList = base.List(sc.ConditionStr, pi);

            if (poList != null && poList.Count > 0)
            {
                var orderCodeArray = (from t in poList
                                      select t.OrderCode).Distinct().ToArray();

                cc.dal.VProductOrderDetail vpod = new VProductOrderDetail();
                List<VProductOrderDetailInfo> detailList = vpod.GetOrderDetailList(string.Join(",", orderCodeArray));

                foreach (var order in poList)
                {
                    var detail = (from t in detailList
                                  where t.OrderCode == order.OrderCode
                                  select t).ToList();

                    order.DetailList = detail;
                }
            }

            iTotalRows = pi.TotalRows;
            iTotalPages = pi.TotalPages;

            return poList;
        }
        #endregion

        #region 卖家订单列表
        public List<VProductOrderInfo> GetBuyersList(cc.common.UserInfo iLoginUser, string iOrderCode, int iOrderStatus, string iSupplierName, string iStartDate, string iEndDate, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN_R", iLoginUser.UserSN, OperateType.In);

            if (!string.IsNullOrEmpty(iOrderCode))
            {
                sc.AddCondition("OrderCode", iOrderCode, OperateType.Equal);
            }

            if (iOrderStatus == 1)
            {
                sc.AddCondition("OrderStatus", "1,11", OperateType.In);
            }
            else if (iOrderStatus == 2)
            {
                sc.AddCondition("OrderStatus", "2,3,31", OperateType.In);
            }
            else if (iOrderStatus > -2)
            {
                sc.AddCondition("OrderStatus", iOrderStatus.ToString(), OperateType.Equal);
            }

            if (!string.IsNullOrEmpty(iSupplierName))
            {
                sc.AddCondition("Supplier", iSupplierName, OperateType.Like);
            }

            if (!string.IsNullOrEmpty(iStartDate))
            {
                DateTime start;

                if (DateTime.TryParse(iStartDate, out start) == true)
                {
                    sc.AddCondition("DateAdded", start.ToString("yyyy-MM-dd 00:00:00"), OperateType.MoreThan);
                }
            }

            if (!string.IsNullOrEmpty(iEndDate))
            {
                DateTime end;

                if (DateTime.TryParse(iEndDate, out end) == true)
                {
                    sc.AddCondition("DateAdded", end.ToString("yyyy-MM-dd 23:59:59"), OperateType.MoreThan);
                }
            }

            PageInfo pi = new PageInfo();
            pi.PageIndex = iPageIndex;
            pi.PageSize = iPageSize;
            List<VProductOrderInfo> poList = base.List(sc.ConditionStr, pi);

            if (poList != null && poList.Count > 0)
            {
                var orderCodeArray = (from t in poList
                                      select t.OrderCode).Distinct().ToArray();

                cc.dal.VProductOrderDetail vpod = new VProductOrderDetail();
                List<VProductOrderDetailInfo> detailList = vpod.GetOrderDetailList(string.Join(",", orderCodeArray));

                foreach (var order in poList)
                {
                    var detail = (from t in detailList
                                  where t.OrderCode == order.OrderCode
                                  select t).ToList();

                    order.DetailList = detail;
                }
            }

            iTotalRows = pi.TotalRows;
            iTotalPages = pi.TotalPages;

            return poList;
        }
        #endregion

        #region 写订单状态流
        public bool SetProductOrderStateFlow(int iUserId, string iOrderCode, string iDesc, int iOrderStatus, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var sp = "proc_ProductOrder_StateFlow";
            var paras = new[]
                    {
                        new SqlParameter("@orderCode", iOrderCode),//订单号
                        new SqlParameter("@desc", iDesc),//
                        new SqlParameter("@status", iOrderStatus),//
                        new SqlParameter("@sysUserID", iUserId),//
                        new SqlParameter("@msg",null)
                        {Direction = ParameterDirection.Output,Size = 200,SqlDbType = SqlDbType.VarChar}
                    };
            SqlHelper.ExecuteNonQuery(cc.common.Sys.SystemConnections.B2bConn, CommandType.StoredProcedure, sp, paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;
        }
        #endregion

        #region 买家取消订单
        public bool BuyersOrderCancel(cc.common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            VProductOrderInfo order = this.GetOrderByOrderCode(iOrderCode);

            if (order == null)
            {
                iErrorMsg = string.Format("没有查询到订单{0}信息", iOrderCode);
                return false;
            }

            if (order.OrderStatus != (int)OrderStatus.WaitPay)
            {
                iErrorMsg = string.Format("只有待付款的订单才能够取消");
                return false;
            }

            if (this.UpdateOrderStatus(iOrderCode, (int)OrderStatus.Canceled) == true)
            {
                this.SetProductOrderStateFlow(iLoginUser.UserId, iOrderCode, "买家取消订单", (int)OrderStatus.Canceled, out iErrorMsg);

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 卖家取消订单
        public bool SellerOrderCancel(cc.common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            VProductOrderInfo order = this.GetOrderByOrderCode(iOrderCode);

            if (order == null)
            {
                iErrorMsg = string.Format("没有查询到订单{0}信息", iOrderCode);
                return false;
            }

            if (order.OrderStatus != (int)OrderStatus.WaitPay)
            {
                iErrorMsg = string.Format("只有待付款的订单才能够取消");
                return false;
            }

            if (this.UpdateOrderStatus(iOrderCode, (int)OrderStatus.Canceled) == true)
            {
                this.SetProductOrderStateFlow(iLoginUser.UserId, iOrderCode, "卖家取消订单", (int)OrderStatus.Canceled, out iErrorMsg);

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 卖家获取出库信息
        public VProductOrderInfo SellerStoreOutBefore(cc.common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            if (string.IsNullOrEmpty(iOrderCode))
            {
                iErrorMsg = "订单号不明确";
                return null;
            }

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN_S", iLoginUser.UserSN, OperateType.Equal);
            sc.AddCondition("OrderCode", iOrderCode, OperateType.Equal);

            VProductOrderInfo po = base.FindByCondition(sc.ConditionStr);

            if (po.OrderStatus != 2 && po.OrderStatus != 31)
            {
                iErrorMsg = "该订单状态不允许出库";
                return null;
            }

            cc.dal.VProductOrderDetail vpod = new VProductOrderDetail();
            List<VProductOrderDetailInfo> detailList = vpod.GetOrderDetailList(iOrderCode);

            if (detailList == null || detailList.Count == 0)
            {
                iErrorMsg = "未查询到订单商品信息";
                return null;
            }

            po.DetailList = detailList;

            return po;
        }
        #endregion

        #region 订单出库
        public bool SellerStoreOut(common.UserInfo iLoginUser, string iOrderCode, string iStoreOutInfo, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var sp = "proc_ProductOrder_StoreOut";
            var paras = new[]
                    {
                        new SqlParameter("@orderCode", iOrderCode),//订单号
                        new SqlParameter("@storeOutInfo", iStoreOutInfo),//
                        new SqlParameter("@userSN_S", iLoginUser.UserSN),//
                        new SqlParameter("@msg",null)
                        {Direction = ParameterDirection.Output,Size = 200,SqlDbType = SqlDbType.VarChar}
                    };
            SqlHelper.ExecuteNonQuery(cc.common.Sys.SystemConnections.B2bConn, CommandType.StoredProcedure, sp, paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;
        }
        #endregion

        #region 确认收货
        public bool ConfirmReceipt(common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;

            iErrorMsg = string.Empty;
            VProductOrderInfo order = this.GetOrderByOrderCode(iOrderCode);

            if (order == null)
            {
                iErrorMsg = string.Format("没有查询到订单{0}信息", iOrderCode);
                return false;
            }

            if (order.OrderStatus != (int)OrderStatus.Delivered)
            {
                iErrorMsg = string.Format("只有待付款的订单才能够取消");
                return false;
            }

            if (this.UpdateOrderStatus(iOrderCode, (int)OrderStatus.Finished) == true)
            {
                this.SetProductOrderStateFlow(iLoginUser.UserId, iOrderCode, "买家确认收货", (int)OrderStatus.Finished, out iErrorMsg);

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 获取指定订单号的订单信息
        public VProductOrderInfo GetOrderByOrderCode(string iOrderCode)
        {
            if (string.IsNullOrEmpty(iOrderCode))
            {
                return null;
            }

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("OrderCode", iOrderCode, OperateType.Equal);

            return base.FindByCondition(sc.ConditionStr);
        }
        #endregion

        #region 更新订单状态
        public bool UpdateOrderStatus(string iOrderCode, int iOrderStatus)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("OrderCode", iOrderCode, OperateType.Equal);

            JObject para = new JObject()
            {
                {"OrderStatus",iOrderStatus},
            };

            return base.UpdateByCondition(para.ToString(), sc.ConditionStr) > 0 ? true : false;
        }
        #endregion

        #region 订单核销
        public bool OrderWriteOff(common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var sp = "proc_ProductOrder_WriteOff";
            var paras = new[]
                    {
                        new SqlParameter("@orderCode", iOrderCode),//订单号
                        new SqlParameter("@userSN", iLoginUser.UserSN),//
                        new SqlParameter("@sysUserId", iLoginUser.UserId),//
                        new SqlParameter("@msg",null)
                        {Direction = ParameterDirection.Output,Size = 200,SqlDbType = SqlDbType.VarChar}
                    };
            SqlHelper.ExecuteNonQuery(cc.common.Sys.SystemConnections.B2bConn, CommandType.StoredProcedure, sp, paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;
        }
        #endregion

        #region 获取订单信息
        public VProductOrderInfo GetOrderInfo(common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            #region 获取订单主表
            if (string.IsNullOrEmpty(iOrderCode))
            {
                iErrorMsg = "订单号不正确";
                return null;
            }

            SearchCondition sc = new SearchCondition();

            sc.AddCondition("OrderCode", iOrderCode, OperateType.Equal);

            VProductOrderInfo result = base.FindByCondition(sc.ConditionStr);
            #endregion

            #region 查询订单细节
            VProductOrderDetail podDal = new VProductOrderDetail();
            List<VProductOrderDetailInfo> details = podDal.GetOrderDetailByOrderCode(iOrderCode);

            result.DetailList = details;
            #endregion

            return result;
        }
        #endregion

        #region 订单改价
        public bool OrderChangePrice(common.UserInfo iLoginUser, string iOrderCode, string iNewTransFee, string iNewProPriceInfo, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var sp = "proc_ProductOrder_ChangePrice";
            var paras = new[]
                    {
                        new SqlParameter("@orderCode", iOrderCode),//订单号
                        new SqlParameter("@newTransFee", iNewTransFee),//订单号
                        new SqlParameter("@newProPriceInfo", iNewProPriceInfo),//订单号
                        new SqlParameter("@userSN", iLoginUser.UserSN),//
                        new SqlParameter("@sysUserId", iLoginUser.UserId),//
                        new SqlParameter("@msg",null)
                        {Direction = ParameterDirection.Output,Size = 200,SqlDbType = SqlDbType.VarChar}
                    };
            SqlHelper.ExecuteNonQuery(cc.common.Sys.SystemConnections.B2bConn, CommandType.StoredProcedure, sp, paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;
        }
        #endregion

        public VProductOrderInfo GetProductOrderInfo(cc.common.UserInfo iLoginUser, string iOrderCode, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            if (string.IsNullOrEmpty(iOrderCode))
            {
                iErrorMsg = "订单号不明确";
                return null;
            }

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN_S", iLoginUser.UserSN, OperateType.Equal);
            sc.AddCondition("OrderCode", iOrderCode, OperateType.Equal);

            VProductOrderInfo po = base.FindByCondition(sc.ConditionStr);

            cc.dal.VProductOrderDetail vpod = new VProductOrderDetail();
            List<VProductOrderDetailInfo> detailList = vpod.GetOrderDetailList(iOrderCode);

            if (detailList == null || detailList.Count == 0)
            {
                iErrorMsg = "未查询到订单商品信息";
                return null;
            }

            po.DetailList = detailList;

            return po;
        }
    }
}
