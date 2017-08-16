using cc.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.ProductOrder.StoreOut
{
    public class StoreOut
    {
        common.UserInfo mLoginUser;
        string mOrderCode;
        string mStoreOutInfoStr;
        List<StoreOutInfo> mStoreOutInfo = new List<StoreOutInfo>();

        VProductOrderInfo mOrder;
        public StoreOut(common.UserInfo iLoginUser, string iOrderCode, string iStoreOutInfoStr)
        {
            this.mLoginUser = iLoginUser;
            this.mOrderCode = iOrderCode;
            this.mStoreOutInfoStr = iStoreOutInfoStr;
        }

        public bool SellerSotreOut(out string iErrorMsg)
        {
            iErrorMsg = string.Empty;

            if (this.SerializeStoreOutInfo(out iErrorMsg) == false)
            {
                return false;
            }

            if (this.LoadProductOrderInfo(out iErrorMsg) == false)
            {
                return false;
            }

            cc.dal.StoreOutDateil sodDal = new dal.StoreOutDateil();
            string storeOutCode = "out" + cc.common.Utility.SerialNumber.CreateOrderCode();

            var transName = "myOrder";
            var sqlconn = new SqlConnection(cc.common.Sys.SystemConnections.B2bConn);
            if (sqlconn.State != ConnectionState.Open)
            {
                sqlconn.Open();
            }

            var trans = sqlconn.BeginTransaction(transName);

            try
            {
                if (this.mStoreOutInfo != null && this.mStoreOutInfo.Count > 0)
                {
                    foreach (var soi in this.mStoreOutInfo)
                    {
                        #region 添加出库细节信息
                        var orderDetailInfo = (from t in this.mOrder.DetailList
                                               where t.ProId == soi.ProId
                                               select t).FirstOrDefault();

                        if (orderDetailInfo.ProCount - orderDetailInfo.StoreOutNum >= soi.ProCount)
                        {
                            sodDal.Insert(this.mLoginUser, soi.ProId, storeOutCode, this.mOrderCode, soi.ProCount, orderDetailInfo.ProPrice, orderDetailInfo.ProCount, trans, out iErrorMsg);
                        }
                        else
                        {
                            iErrorMsg = "出库数量大于商品数量";
                            return false;
                        }
                        #endregion
                    }

                    int sumProCount = (from t in this.mStoreOutInfo
                                       select t.ProCount).Sum();

                    #region 添加出库信息
                    cc.dal.StoreOut soDal = new dal.StoreOut();
                    soDal.Insert(this.mLoginUser, this.mOrderCode, storeOutCode, sumProCount, this.mOrder.UserSN_R, trans, out iErrorMsg);
                    #endregion
                }
                else
                {
                    iErrorMsg = "出库信息读取错误";
                    return false;
                }

                if (!string.IsNullOrEmpty(iErrorMsg))
                {
                    trans.Rollback(transName);
                    return false;
                }
                else
                {
                    trans.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback(transName);
                iErrorMsg = ex.ToString();
                cc.log.Log.Error(typeof(StoreOut), ex);
                return false;
            }
            finally
            {
                trans.Dispose();
                sqlconn.Close();
            }

            return true;
        }

        public bool LoadProductOrderInfo(out string iErrorMsg)
        {
            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            this.mOrder = poDal.SellerStoreOutBefore(this.mLoginUser, this.mOrderCode, out iErrorMsg);

            if (string.IsNullOrEmpty(iErrorMsg))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 序列化出库商品信息
        /// </summary>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public bool SerializeStoreOutInfo(out string iErrorMsg)
        {
            iErrorMsg = string.Empty;

            if (string.IsNullOrEmpty(this.mStoreOutInfoStr))
            {
                iErrorMsg = "请选择要出库的商品信息和出库数量";
                return false;
            }

            string[] array1 = this.mStoreOutInfoStr.Split(',');

            if (array1 != null && array1.Length > 0)
            {
                foreach (string str1 in array1)
                {
                    string[] array2 = str1.Split('|');

                    if (array2 != null && array2.Length == 2)
                    {
                        int proId = 0;
                        int proCount = 0;

                        if (int.TryParse(array2[0], out proId) && int.TryParse(array2[1], out proCount) && proId > 0)
                        {
                            this.mStoreOutInfo.Add(new StoreOutInfo(proId, proCount));
                        }
                        else
                        {
                            iErrorMsg = "格式化出库商品信息出错";
                            return false;
                        }
                    }
                    else
                    {
                        iErrorMsg = "格式化出库商品信息出错";
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
