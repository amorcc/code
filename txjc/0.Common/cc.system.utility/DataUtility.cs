using my.core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my.system.utility
{
    public class DataUtility
    {
        /// <summary>
        /// 执行存储过程，存储过程最后两个参数必须是@sysUserId和@msg
        /// </summary>
        /// <param name="jsonPara">json参数也是存储过程的参数</param>
        /// <param name="userId">操作人id</param>
        /// <param name="spName">存储过程名字</param>
        /// <param name="sucMsg">成功后提示信息</param>
        /// <param name="sqlCon">数据连接，默认B2B</param>
        /// <returns>如果执行成功，无返回值</returns>
        public static string ExecuteNonQuery(string jsonPara, int userId, string spName, string sucMsg = null, string sqlCon = null)
        {
            var Payload = new StandardPayload();             //标准化返回字符串格式
            var jo = JObject.Parse(jsonPara);
            string[] fields = jo.Properties().Select(item => item.Name.ToString()).ToArray();
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            SqlParameter[] param = new SqlParameter[fields.Length + 2];
            int i = 0;
            while (i < fields.Length)
            {
                param[i] = new SqlParameter("@" + fields[i], values[i]);
                i++;
            }
            param[i] = new SqlParameter("@sysUserID", userId);
            param[i + 1] = new SqlParameter("@msg", null) { Direction = ParameterDirection.Output, Size = 200, SqlDbType = SqlDbType.VarChar };
            var con = new SqlConnection(string.IsNullOrEmpty(sqlCon) ? SystemConnections.B2bConn : sqlCon);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, spName, param);
                var msg = param[i + 1].Value.ToString();
                if (string.IsNullOrEmpty(msg))
                {
                    Payload.Message = string.IsNullOrEmpty(sucMsg) ? "操作成功" : sucMsg;
                }
                else
                {
                    Payload.ResponseID = 1;
                    Payload.Message = "操作失败";
                }
            }
            catch (Exception ex)
            {
                Payload.ResponseID = 1;
                Payload.Message = "操作失败，请稍后重试！";
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
                con.Dispose();
            }
            return JsonConvert.SerializeObject(Payload);//返回操作结果
        }
        /// <summary>
        /// 执行存储过程返回一个DataSet
        /// </summary>
        /// <param name="jsonPara">查询参数</param>
        /// <param name="userId">当前操作人Id</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="sqlCon">数据链接</param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(string jsonPara, int userId, string spName, string sqlCon = null)
        {
            DataSet ds = null;
            var Payload = new StandardPayload();             //标准化返回字符串格式
            var jo = JObject.Parse(jsonPara);
            string[] fields = jo.Properties().Select(item => item.Name.ToString()).ToArray();
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            SqlParameter[] param = new SqlParameter[fields.Length + 2];
            int i = 0;
            while (i < fields.Length)
            {
                param[i] = new SqlParameter("@" + fields[i], values[i]);
                i++;
            }
            param[i] = new SqlParameter("@sysUserID", userId);
            param[i + 1] = new SqlParameter("@msg", null) { Direction = ParameterDirection.Output, Size = 200, SqlDbType = SqlDbType.VarChar };
            var con = new SqlConnection(string.IsNullOrEmpty(sqlCon) ? SystemConnections.B2bConn : sqlCon);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, spName, param);
                var msg = param[i + 1].Value.ToString();
                if (!string.IsNullOrEmpty(msg))
                {
                    return null;
                }
                else
                {
                    return ds;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
                con.Dispose();
            }
        }
    }
}
