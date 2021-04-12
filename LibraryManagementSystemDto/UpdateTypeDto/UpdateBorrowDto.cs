using System;

namespace LibraryManagementSystem.DTO.UpdateTypeDto
{
    /// <summary>
    /// 图书借阅Dto
    /// </summary>
    public class UpdateBorrowDto    
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
    }
}