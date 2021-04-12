using System;

namespace LibraryManagementSystem.MODEL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.MODEL
    * 文件名称  :FinePayment.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-09 15:45:34 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class FinePayment:BaseEntity
    {
        /// <summary>
        /// 借阅编号
        /// </summary>
        public Guid BorrowId { get; set; }
        
        /// <summary>
        /// 罚款金额
        /// </summary>
        public decimal Penalty { get; set; }

        /// <summary>
        /// 是否缴纳罚款
        /// </summary>
        public bool IsPay { get; set; }

        /// <summary>
        /// 支付-方式
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        public Guid AdminId { get; set; }
        
    }
}
