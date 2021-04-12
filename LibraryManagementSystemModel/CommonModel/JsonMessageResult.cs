using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.MODEL.CommonModel
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.MODEL.Common
    * 文件名称  :JsonMessageResult.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-03 16:11:03 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class JsonMessageResult
    {
        public JsonMessageResult(string resultMessage, int resultCode, object resultData)
        {
            ResultMessage = resultMessage;
            ResultCode = resultCode;
            ResultData = resultData;
        }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ResultMessage { get; set; }

        /// <summary>
        /// 返回状态  Code==1?Success:Fail 
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object ResultData { get; set; }
    }
}
