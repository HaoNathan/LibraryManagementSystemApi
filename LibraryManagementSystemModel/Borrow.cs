using System;

namespace LibraryManagementSystem.MODEL
{
    /// <summary>
    /// 图书借阅实体类
    /// </summary>
    public class Borrow:BaseEntity
    {
        ///<summary>
        ///学生编号
        /// </summary>
        public Guid StudentId { get; set; }

        ///<summary>
        ///到期时间
        /// </summary>
        public DateTime EndTime { get; set; }

        ///<summary>
        ///归还时间
        /// </summary>
        public DateTime BackTime { get; set; }

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