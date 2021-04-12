using System;

namespace LibraryManagementSystem.MODEL
{
    /// <summary>
    /// 书籍实体类
    /// </summary>
    public class Book:BaseEntity
    {
        
        ///<summary>
        ///书籍详细信息
        /// </summary>
        public Guid BookInfoId { get; set; }

        ///<summary>
        ///书籍状态 0:正常 1:借阅 2:预约
        /// </summary>
        public int BookState { get; set; }

    }
}