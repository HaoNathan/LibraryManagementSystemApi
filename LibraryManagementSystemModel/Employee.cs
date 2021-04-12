using System;

namespace LibraryManagementSystem.MODEL
{
    /// <summary>
    /// 员工信息实体类
    /// </summary>
    public class Employee:BaseEntity
    {
        ///<summary>
        ///年龄
        /// </summary>
        public int Age { get; set; }

        ///<summary>
        ///所在部门
        /// </summary>
        public Guid DepartmentId { get; set; }

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