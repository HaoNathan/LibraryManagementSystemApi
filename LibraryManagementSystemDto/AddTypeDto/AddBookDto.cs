using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.AddTypeDto
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.DTO.AddTypeDto
    * 文件名称  :AddBookDto.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 17:25:57 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class AddBookDto
    {
        ///<summary>
        ///书籍详细信息
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("书籍信息编号")]
        public Guid BookInfoId { get; set; }

        ///<summary>
        ///书籍状态
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("书籍状态")]
        public int BookState { get; set; }
    }
}
