using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DTO.AddTypeDto
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.DTO.AddTypeDto
    * 文件名称  :AddBookInventoryDto.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-20 12:34:13 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class AddBookInventoryDto
    {
        ///<summary>
        /// 书籍信息编号
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("类别编号")]
        public Guid BookInfoId { get; set; }

        ///<summary>
        /// 登记人员
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("登记人员")]
        public Guid AdminId { get; set; }

        ///<summary>
        ///入库数量
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("入库数量")]
        public int InventoryQuantity { get; set; }
    }
}
