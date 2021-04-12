using System;

namespace LibraryManagementSystem.DTO.QueryTypeDto
{
    /// <summary>
    /// 员工信息实体类
    /// </summary>
    public class EmployeeDto
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
        ///年龄
        /// </summary>
        public int Age { get; set; }

        ///<summary>
        ///所在部门
        /// </summary>
        public Guid DepartmentId { get; set; }
        
        /// <summary>
        /// 部门名称
        /// </summary>
        public string CompanyDepartmentName { get; set; }

        ///<summary>
        ///出生年月
        /// </summary>
        public DateTime BirthDay { get; set; }

        ///<summary>
        ///性别
        /// </summary>
        public bool Sex { get; set; }

        ///<summary>
        ///员工姓名
        /// </summary>
        public string EmployeeName { get; set; }

        ///<summary>
        ///联系方式
        /// </summary>
        public string Contact { get; set; }


    }
}