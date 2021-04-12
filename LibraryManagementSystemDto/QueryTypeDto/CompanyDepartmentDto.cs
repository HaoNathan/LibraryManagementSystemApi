using System;

namespace LibraryManagementSystem.DTO.QueryTypeDto
{
    /// <summary>
    /// 员工部门实体类
    /// </summary>
    public class CompanyDepartmentDto
    {
        /// <summary>
        /// ID  
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
        ///是否移除
        /// </summary>
        public bool IsRemove { get; set; }

        ///<summary>
        ///部门名称
        /// </summary>
        public string CompanyDepartmentName { get; set; }

    }
}