using System;

namespace LibraryManagementSystem.DTO.UpdateTypeDto
{
    /// <summary>
    /// 图书预约实体类
    /// </summary>
    public class UpdateReservationDto
    {
        /// <summary>
        ///是否移除
        /// </summary>
        public bool IsRemove { get; set; }

        ///<summary>
        ///学生编号
        /// </summary>
        public Guid StudentId { get; set; }

        ///<summary>
        ///预约时间
        /// </summary>
        public DateTime ReservationTime { get; set; }
    }
}