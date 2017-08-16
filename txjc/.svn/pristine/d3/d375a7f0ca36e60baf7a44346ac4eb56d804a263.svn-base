using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.common.Utility
{
    /// <summary>
    /// 登录返回值
    /// </summary>
    public class ActionResult<T>
    {
        public ResponseResult ResponseID;
        public string Message;
        public T Data;
    }

    /// <summary>
    /// 执行结果
    /// </summary>
    public enum ResponseResult
    {
        /// <summary>
        /// 未知错误类型
        /// </summary>
        None = -1,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 需要重新登录
        /// </summary>
        MustLogin = 1,
        /// <summary>
        /// 业务规则错误信息
        /// </summary>
        BusinessError = 2,
        /// <summary>
        /// 用户名不存在
        /// </summary>
        UserNameNonExist = 1001,
        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError = 1002,
        /// <summary>
        /// 指定登录角色不存在
        /// </summary>
        RoleNonExist = 1003,
    }

    public class MyResponse
    {
        /// <summary>
        /// 正常返回数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iData"></param>
        /// <param name="iResponse"></param>
        /// <param name="iMessage"></param>
        /// <returns></returns>
        public static ActionResult<T> ToYou<T>(T iData, string iMessage = "", ResponseResult iResponse = ResponseResult.Success)
        {
            ActionResult<T> result = new ActionResult<T>();

            result.ResponseID = iResponse;
            result.Message = iMessage;
            result.Data = iData;

            return result;
        }

        /// <summary>
        /// Token错误或不存在，需要重新登录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ActionResult<T> MustLogin<T>()
        {
            return ToYou(default(T), "请重新登录！", ResponseResult.MustLogin);
        }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ActionResult<T> ShowError<T>(string msg)
        {
            return ToYou(default(T), msg, ResponseResult.None);
        }

        public static ActionResult<T> ShowBusinessError<T>(string msg)
        {
            return ToYou(default(T), msg, ResponseResult.BusinessError);
        }
    }

}
