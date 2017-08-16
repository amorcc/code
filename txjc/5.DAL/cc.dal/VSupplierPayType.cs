using cc.basedal;
using cc.common.DataConvert;
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
    public class VSupplierPayType : BaseDAL<VSupplierPayTypeInfo>
    {
        public VSupplierPayType()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "v_SupplierPayType";
        }

        protected override VSupplierPayTypeInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new VSupplierPayTypeInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.UserSN = Convert.ToString(dr["UserSN"] == DBNull.Value ? "" : dr["UserSN"]);
            obj.PayTypeDesc = Convert.ToString(dr["PayTypeDesc"] == DBNull.Value ? "" : dr["PayTypeDesc"]);
            obj.PayTypeId = Convert.ToInt32(dr["PayTypeId"] == DBNull.Value ? 0 : dr["PayTypeId"]);
            obj.SupplierRate = Convert.ToDecimal(dr["SupplierRate"] == DBNull.Value ? 0 : dr["SupplierRate"]);
            obj.ClientType = Convert.ToInt32(dr["ClientType"] == DBNull.Value ? 0 : dr["ClientType"]);


            return obj;
        }

        public List<VSupplierPayTypeInfo> GetSupplierPayType(List<string> iSupplierUserSNList)
        {
            string userSNs = string.Join(",", iSupplierUserSNList.ToArray());

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN", userSNs, OperateType.In);

            return base.List(sc.ConditionStr);
        }

        public List<PayTypeInfo> GetCommonPayType(List<string> iSupplierUserSNList)
        {
            List<PayTypeInfo> result = new List<PayTypeInfo>();
            if (iSupplierUserSNList == null || iSupplierUserSNList.Count == 0)
            {
                return new List<PayTypeInfo>();
            }

            List<VSupplierPayTypeInfo> list = this.GetSupplierPayType(iSupplierUserSNList);

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    int payType = item.PayTypeId;

                    int count = (from t in list
                                 where t.PayTypeId == payType
                                 select t.Id).Count();

                    if (count >= iSupplierUserSNList.Count)
                    {
                        bool isExist = (from t in result
                                        where t.PayTypeId == payType
                                        select t).Count() > 0 ? true : false;

                        if (isExist == false)
                        {
                            result.Add(new PayTypeInfo(item.PayTypeId, item.PayTypeDesc));
                        }
                    }
                }

            }

            return result;
        }

        /// <summary>
        /// 获取卖家的支付配置信息
        /// </summary>
        /// <param name="iLoginUser"></param>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public JArray GetSupplierPayType(cc.common.UserInfo iLoginUser, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            try
            {
                var sp = "[proc_Supplier_GetPayType]";
                var paras = new[]
                    {
                        new SqlParameter("@userSN_S", iLoginUser.UserSN),
                        new SqlParameter("@sysUserID", iLoginUser.UserId), //权限控制 及参数列表
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}
                    };
                using (DataSet ds = SqlHelper.ExecuteDataset(this._sqlCon, CommandType.StoredProcedure, sp, paras))
                {
                    var msg = paras[paras.Length - 1].Value;
                    if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                    {
                        iErrorMsg = msg.ToString();
                        return null;
                    }

                    if (ds != null && ds.Tables[0] != null)
                    {
                        JArray result = new JArray();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            int isOpen = 0;

                            if (row["SupplierPayId"] != DBNull.Value && DataConvert.ToInt32(row["SupplierPayId"]) > 0)
                            {
                                isOpen = 1;
                            }

                            result.Add(new JObject()
                            {
                                {"SysPayTypeId", DataConvert.ToInt32(row["SysPayTypeId"])},
                                {"PayTypeDesc", DataConvert.ToString(row["PayTypeDesc"])},
                                {"Rate", DataConvert.ToDecimal(row["Rate"])},
                                {"IsEnable", DataConvert.ToInt32(row["IsEnable"])},
                                {"Memo", DataConvert.ToString(row["Memo"])},
                                {"SupplierPayId", DataConvert.ToInt32(row["SupplierPayId"])},
                                {"UserSN", DataConvert.ToString(row["UserSN"])},
                                {"IsOpen", isOpen},
                            });

                        }
                        return result;
                    }
                    else
                    {
                        iErrorMsg = "未获取到支付信息，请稍候重试";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                iErrorMsg = ex.ToString();
                return null;
            }
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
            iErrorMsg = string.Empty;
            try
            {
                var sp = "[proc_Supplier_SetPayType]";
                var paras = new[]
                    {
                        new SqlParameter("@userSN_S", iLoginUser.UserSN),
                        new SqlParameter("@payTypeId", iPayTypeId),
                        new SqlParameter("@isOpen", iIsOpen),
                        new SqlParameter("@sysUserID", iLoginUser.UserId), //权限控制 及参数列表
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}
                    };
                SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, sp, paras);
                var msg = paras[paras.Length - 1].Value;
                if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                {
                    iErrorMsg = msg.ToString();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                iErrorMsg = ex.ToString();
                return false;
            }
        }
    }


}
