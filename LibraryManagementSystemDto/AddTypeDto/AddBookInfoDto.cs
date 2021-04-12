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
    * 创建日期  :2021-02-16 15:34:43 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class AddBookInfoDto
    {
        ///<summary>
        ///书籍类型
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("类别编号")]
        public Guid BookCategoryId { get; set; }

        ///<summary>
        ///出版社
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("出版社编号")]
        public Guid PublishingId { get; set; }

        ///<summary>
        ///价格
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("价格")]
        public decimal Price { get; set; }

        ///<summary>
        ///发布时间
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("发布时间")]
        public DateTime ReleaseDate { get; set; }

        ///<summary>
        ///书籍名
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("书籍名称")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string BookName { get; set; }

        ///<summary>
        ///封面
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("书籍封面")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string BookPhoto { get; set; }

        ///<summary>
        ///作者
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("作者")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string Author { get; set; }

        /// <summary>
        /// ISBN号
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("ISBN号")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "{0}的长度应该是{1}")]
        public string ISBN { get; set; }
    }
}
