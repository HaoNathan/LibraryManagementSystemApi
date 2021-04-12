using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.UpdateTypeDto
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
    public class UpdateFinePaymentDto
    {
        
        /// <summary>
        ///是否移除
        /// </summary>
        public bool IsRemove { get; set; }

        /// <summary>
        /// 借阅编号
        /// </summary>
        public Guid BorrowId { get; set; }
        
        /// <summary>
        /// 罚款金额
        /// </summary>
        public double Penalty { get; set; }

        /// <summary>
        /// 是否缴纳罚款
        /// </summary>
        public bool IsPay { get; set; }

        /// <summary>
        /// 支付-方式
        /// </summary>
        [DisplayName("支付方式")]
        [StringLength(10,MinimumLength = 2,ErrorMessage = "{0}的长度应介于{2}至{1}之间！")]
        public string PayType { get; set; }        
    }
}
