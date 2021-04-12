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
    * 文件名称  :AddReservationDto
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-03-11 15:32:10
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    /// <summary>
    /// 图书预约实体类
    /// </summary>
    public class AddReservationDto
    {
        ///<summary>
        ///学生编号
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("学生编号")]
        public Guid StudentId { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("管理员编号")]
        public Guid AdminId { get; set; }

        /// <summary>
        /// 书籍Id
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("书籍编号")]
        public Guid BookId { get; set; }
    }
}
