using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.AddTypeDto
{
    /// <summary>
    /// 员工信息实体类
    /// </summary>
    public class AddEmployeeDto
    {
        ///<summary>
        ///年龄
        /// </summary>
        [DisplayName("年龄")]
        [Range(1, 100, ErrorMessage = "{0}应介于{1}至{2}")]
        public int Age { get; set; }

        ///<summary>
        ///所在部门
        /// </summary>
        public Guid DepartmentId { get; set; }

        ///<summary>
        ///出生年月
        /// </summary>
        [Required(ErrorMessage = "")]
        [DisplayName("出生年月")]
        public DateTime BirthDay { get; set; }

        ///<summary>
        ///性别
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("性别")]
        public bool Sex { get; set; }

        ///<summary>
        ///员工姓名
        /// </summary>
        [DisplayName("员工姓名")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0}是必须的")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string EmployeeName { get; set; }

        ///<summary>
        ///联系方式
        /// </summary>
        [Phone(ErrorMessage = "请输入有效的{0}！")]
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("联系方式")]
        public string Contact { get; set; }
    }
}