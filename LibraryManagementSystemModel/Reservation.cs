using System;

namespace LibraryManagementSystem.MODEL
{
    /// <summary>
    /// 图书预约实体类
    /// </summary>
    public class Reservation:BaseEntity
    {
        ///<summary>
        ///学生编号
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        public Guid AdminId { get; set; }

        /// <summary>
        /// 书籍Id
        /// </summary>
        public Guid BookId { get; set; }
    }
}