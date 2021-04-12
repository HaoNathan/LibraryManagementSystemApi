using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.AddTypeDto
{
    /// <summary>
    /// 图书借阅Dto
    /// </summary>
    public class AddBorrowDto    
    {
        ///<summary>
        ///学生编号
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("学生编号")]
        public Guid StudentId { get; set; }

        ///<summary>
        ///到期时间
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("到期时间")]
        public DateTime EndTime { get; set; }

        ///<summary>
        ///归还时间
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("归还时间")]
        public DateTime BackTime { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("管理员")]
        public Guid AdminId { get; set; }

        /// <summary>
        /// 书籍Id
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("书籍编号")]
        public Guid BookId { get; set; }

    }
}