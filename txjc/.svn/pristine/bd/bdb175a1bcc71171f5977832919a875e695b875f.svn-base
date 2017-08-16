using cc.basedal;
using cc.model;
using cc.common.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace cc.dal
{
    public class User : BaseDAL<UserInfo>
    {
        public User()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "[User]";
        }

        protected override model.UserInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new model.UserInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.Email = Convert.ToString(dr["Email"] == DBNull.Value ? "" : dr["Email"]);
            obj.Openid = Convert.ToString(dr["Openid"] == DBNull.Value ? "" : dr["Openid"]);
            obj.Phone = Convert.ToString(dr["Phone"] == DBNull.Value ? "" : dr["Phone"]);
            obj.RealName = Convert.ToString(dr["RealName"] == DBNull.Value ? "" : dr["RealName"]);
            obj.RegisterSource = Convert.ToInt32(dr["RegisterSource"] == DBNull.Value ? 0 : dr["RegisterSource"]);
            obj.Tier = Convert.ToString(dr["Tier"] == DBNull.Value ? "" : dr["Tier"]);
            obj.UserName = Convert.ToString(dr["UserName"] == DBNull.Value ? "" : dr["UserName"]);
            obj.UserSN = Convert.ToString(dr["UserSN"] == DBNull.Value ? "" : dr["UserSN"]);
            obj.IsAdmin = Convert.ToInt32(dr["IsAdmin"] == DBNull.Value ? 0 : dr["IsAdmin"]);

            return obj;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="iUserName"></param>
        /// <param name="iPassword"></param>
        /// <param name="iToken"></param>
        /// <returns></returns>
        public common.UserInfo LoginByOpenid(string iOpenid, string iToken, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            if (string.IsNullOrEmpty(iOpenid))
            {
                iErrorMsg = "没有该openid!";
                return null;
            }

            if (string.IsNullOrEmpty(iToken))
            {
                iErrorMsg = "唯一表示不能为空！";
                return null;
            }

            var paras = new[]
                    {
                        new SqlParameter("@openid", iOpenid),
                        new SqlParameter("@token", iToken),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            using (DataSet ds = SqlHelper.ExecuteDataset(this._sqlCon, CommandType.StoredProcedure, "[proc_User_Login_By_Openid]", paras))
            {
                var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
                if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                {
                    iErrorMsg = msg.ToString();
                    return null;
                }

                if (ds != null && ds.Tables.Count > 1)
                {
                    DataTable dtUser = ds.Tables[0];
                    DataTable dtCompany = ds.Tables[1];

                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        cc.common.UserInfo userInfo = new common.UserInfo();
                        DataRow row = dtUser.Rows[0];

                        userInfo.UserDateAdded = Convert.ToDateTime(row["DateAdded"] == DBNull.Value ? "1900-01-01 00:00:00" : row["DateAdded"]);
                        userInfo.Email = Convert.ToString(row["Email"] == DBNull.Value ? "" : row["Email"]);
                        userInfo.UserId = Convert.ToInt32(row["Id"] == DBNull.Value ? 0 : row["Id"]);
                        userInfo.IsAdmin = Convert.ToInt32(row["IsAdmin"] == DBNull.Value ? 0 : row["IsAdmin"]);
                        userInfo.Phone = Convert.ToString(row["Phone"] == DBNull.Value ? "" : row["Phone"]);
                        userInfo.RealName = Convert.ToString(row["RealName"] == DBNull.Value ? "" : row["RealName"]);
                        userInfo.RegisterSource = Convert.ToInt32(row["RegisterSource"] == DBNull.Value ? 0 : row["RegisterSource"]);
                        userInfo.Tier = Convert.ToString(row["Tier"] == DBNull.Value ? "" : row["Tier"]);
                        userInfo.UserName = Convert.ToString(row["UserName"] == DBNull.Value ? "" : row["UserName"]);
                        userInfo.UserSN = Convert.ToString(row["UserSN"] == DBNull.Value ? "" : row["UserSN"]);
                        userInfo.Openid = Convert.ToString(row["Openid"] == DBNull.Value ? "" : row["Openid"]);
                        userInfo.Token = iToken;

                        #region 加载用户公司信息

                        if (dtCompany != null && dtCompany.Rows.Count > 0)
                        {
                            DataRow row1 = dtCompany.Rows[0];
                            userInfo.IsExistCompanyInfo = true;

                            userInfo.CompanyDateAdded = Convert.ToDateTime(row1["DateAdded"] == DBNull.Value ? "1900-01-01 00:00:00" : row1["DateAdded"]);
                            userInfo.CompanyName = Convert.ToString(row1["CompanyName"] == DBNull.Value ? "" : row1["CompanyName"]);
                            userInfo.AreaCode = Convert.ToInt32(row1["AreaCode"] == DBNull.Value ? 0 : row1["AreaCode"]);
                            userInfo.CompanyPhone = Convert.ToString(row1["CompanyPhone"] == DBNull.Value ? "" : row1["CompanyPhone"]);
                            userInfo.CompanyAddress = Convert.ToString(row1["CompanyAddress"] == DBNull.Value ? "" : row1["CompanyAddress"]);
                            userInfo.BusinessScope = Convert.ToString(row1["BusinessScope"] == DBNull.Value ? "" : row1["BusinessScope"]);
                            userInfo.IsOpenSupplier = Convert.ToInt32(row1["IsOpenSupplier"] == DBNull.Value ? 0 : row1["IsOpenSupplier"]);
                        }
                        else
                        {
                            userInfo.IsExistCompanyInfo = false;
                        }
                        #endregion

                        return userInfo;
                    }
                    else
                    {
                        iErrorMsg = "系统出错，未读取到用户信息！";
                        return null;
                    }


                }
            }

            //SqlHelper.ExecuteNonQuery(zmm.system.utility.SystemConnections.B2bConn, CommandType.StoredProcedure, "proc_Supplier_PayType_Setup", paras);
            //var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            //if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            //{
            //    iErrorMsg = msg.ToString();
            //    return false;
            //}

            return null;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="iUserName"></param>
        /// <param name="iPassword"></param>
        /// <param name="iToken"></param>
        /// <returns></returns>
        public common.UserInfo Login(string iUserName, string iPassword, string iToken, string iOpenid, string access_token, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            if (string.IsNullOrEmpty(iUserName))
            {
                iErrorMsg = "用户名不能为空!";
                return null;
            }

            if (string.IsNullOrEmpty(iPassword))
            {
                iErrorMsg = "密码不能为空！";
                return null;
            }

            if (string.IsNullOrEmpty(iToken))
            {
                iErrorMsg = "唯一表示不能为空！";
                return null;
            }



            iPassword = common.Sys.MyMD5.CreateMD5Hash(iPassword);

            var paras = new[]
                    {
                        new SqlParameter("@userName", iUserName),
                        new SqlParameter("@pwd", iPassword),
                        new SqlParameter("@openid", iOpenid),
                        new SqlParameter("@token", iToken),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            using (DataSet ds = SqlHelper.ExecuteDataset(this._sqlCon, CommandType.StoredProcedure, "proc_User_Login", paras))
            {
                var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
                if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                {
                    iErrorMsg = msg.ToString();
                    return null;
                }

                if (ds != null && ds.Tables.Count > 1)
                {
                    DataTable dtUser = ds.Tables[0];
                    DataTable dtCompany = ds.Tables[1];

                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        cc.common.UserInfo userInfo = new common.UserInfo();
                        DataRow row = dtUser.Rows[0];

                        userInfo.UserDateAdded = Convert.ToDateTime(row["DateAdded"] == DBNull.Value ? "1900-01-01 00:00:00" : row["DateAdded"]);
                        userInfo.Email = Convert.ToString(row["Email"] == DBNull.Value ? "" : row["Email"]);
                        userInfo.UserId = Convert.ToInt32(row["Id"] == DBNull.Value ? 0 : row["Id"]);
                        userInfo.IsAdmin = Convert.ToInt32(row["IsAdmin"] == DBNull.Value ? 0 : row["IsAdmin"]);
                        userInfo.Phone = Convert.ToString(row["Phone"] == DBNull.Value ? "" : row["Phone"]);
                        userInfo.RealName = Convert.ToString(row["RealName"] == DBNull.Value ? "" : row["RealName"]);
                        userInfo.RegisterSource = Convert.ToInt32(row["RegisterSource"] == DBNull.Value ? 0 : row["RegisterSource"]);
                        userInfo.Tier = Convert.ToString(row["Tier"] == DBNull.Value ? "" : row["Tier"]);
                        userInfo.UserName = Convert.ToString(row["UserName"] == DBNull.Value ? "" : row["UserName"]);
                        userInfo.UserSN = Convert.ToString(row["UserSN"] == DBNull.Value ? "" : row["UserSN"]);
                        userInfo.Openid = Convert.ToString(row["Openid"] == DBNull.Value ? "" : row["Openid"]);
                        userInfo.Token = iToken;

                        #region 加载用户公司信息

                        if (dtCompany != null && dtCompany.Rows.Count > 0)
                        {
                            DataRow row1 = dtCompany.Rows[0];
                            userInfo.IsExistCompanyInfo = true;

                            userInfo.CompanyDateAdded = Convert.ToDateTime(row1["DateAdded"] == DBNull.Value ? "1900-01-01 00:00:00" : row1["DateAdded"]);
                            userInfo.CompanyName = Convert.ToString(row1["CompanyName"] == DBNull.Value ? "" : row1["CompanyName"]);
                            userInfo.AreaCode = Convert.ToInt32(row1["AreaCode"] == DBNull.Value ? 0 : row1["AreaCode"]);
                            userInfo.CompanyPhone = Convert.ToString(row1["CompanyPhone"] == DBNull.Value ? "" : row1["CompanyPhone"]);
                            userInfo.CompanyAddress = Convert.ToString(row1["CompanyAddress"] == DBNull.Value ? "" : row1["CompanyAddress"]);
                            userInfo.BusinessScope = Convert.ToString(row1["BusinessScope"] == DBNull.Value ? "" : row1["BusinessScope"]);
                            userInfo.IsOpenSupplier = Convert.ToInt32(row1["IsOpenSupplier"] == DBNull.Value ? 0 : row1["IsOpenSupplier"]);
                        }
                        else
                        {
                            userInfo.IsExistCompanyInfo = false;
                        }
                        #endregion

                        //if (!string.IsNullOrEmpty(iOpenid) && !string.IsNullOrEmpty(access_token))
                        //{
                        //    #region 到微信去获取用户信息
                        //    var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", access_token, iOpenid);

                        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                        //    request.Method = "GET";

                        //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                        //    Stream ioStream = response.GetResponseStream();
                        //    StreamReader sr = new StreamReader(ioStream, System.Text.UTF8Encoding.UTF8);
                        //    string html = sr.ReadToEnd();
                        //    sr.Close();
                        //    ioStream.Close();
                        //    response.Close();

                        //    JObject joWxUserInfo = JObject.Parse(html);

                        //    string nickname = joWxUserInfo.GetValueExt<string>("nickname", "");
                        //    string headimgurl = joWxUserInfo.GetValueExt<string>("headimgurl", "");

                        //    if (!string.IsNullOrEmpty(nickname) || !string.IsNullOrEmpty(headimgurl))
                        //    {
                        //        JObject para = new JObject()
                        //        {
                        //            {"NickName",nickname},
                        //            {"HeadImgUrl",headimgurl},
                        //        };
                        //        SearchCondition sc = new SearchCondition();
                        //        sc.AddCondition("Id", userInfo.UserId.ToString(), OperateType.Equal);

                        //        base.UpdateByCondition(para.ToString(), sc.ConditionStr);
                        //    }
                        //    #endregion
                        //}

                        return userInfo;
                    }
                    else
                    {
                        iErrorMsg = "系统出错，未读取到用户信息！";
                        return null;
                    }


                }
            }

            //SqlHelper.ExecuteNonQuery(zmm.system.utility.SystemConnections.B2bConn, CommandType.StoredProcedure, "proc_Supplier_PayType_Setup", paras);
            //var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            //if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            //{
            //    iErrorMsg = msg.ToString();
            //    return false;
            //}

            return null;
        }

        public List<MenuInfo> GetUserMenu(cc.common.UserInfo iLoginUser, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            Menu menuDal = new Menu();
            return menuDal.GetAll();
        }

        public bool ApplyOpenSupplier(common.UserInfo iLoginUser, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var paras = new[]
                    {
                        new SqlParameter("@userSN", iLoginUser.UserSN),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, "[proc_Company_OpenSupplier]", paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;

        }

        public bool IsExsitUserSN(string iUserSN)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN", iUserSN, OperateType.Equal);

            return base.GetCount(sc.ConditionStr) > 0 ? true : false;
        }

        public string GetNewUserSN()
        {
            string userSN = string.Empty;

            Random ran = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));

            do
            {
                userSN = ran.Next(10000000, 99999999).ToString();
            }
            while (this.IsExsitUserSN(userSN) == true);

            return userSN;
        }

        public bool Reg(string iUserName, string iPassword, int iRegSource, string iCompanyName, string iInviteCode, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;

            iPassword = common.Sys.MyMD5.CreateMD5Hash(iPassword);

            string userSN = this.GetNewUserSN();
            var paras = new[]
                    {
                        new SqlParameter("@userName", iUserName),
                        new SqlParameter("@userSN", userSN),
                        new SqlParameter("@pwd", iPassword),
                        new SqlParameter("@RegisterSource", iRegSource),
                        new SqlParameter("@companyName", iCompanyName),
                        new SqlParameter("@inviteCode", iInviteCode),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, "[proc_User_Registration]", paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;

        }
    }
}
