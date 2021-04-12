using System;

namespace LibraryManagementSystem.DTO.QueryTypeDto
{
    /// <summary>
    /// 系部实体类
    /// </summary>
    public class DepartmentDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否作废
        /// </summary>
        public bool IsRemove { get; set; }

        ///<summary>
        ///系部名称
        /// </summary>
        public string DepartmentName { get; set; }
    }
}