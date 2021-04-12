using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.UpdateTypeDto
{
    /// <summary>
    /// 学生信息实体类
    /// </summary>
    public class UpdateStudentDto
    {
        ///<summary>
        ///年龄
        /// </summary>
        [DisplayName("年龄")]
        [Range(1, 100, ErrorMessage = "{0}应介于{1}至{2}")]
        public int Age { get; set; }

        ///<summary>
        ///所在系部
        /// </summary>
        [DisplayName("系部Id")]
        public Guid DepartmentId { get; set; }

        ///<summary>
        ///出生年月
        /// </summary>
        [DisplayName("出生日期")]
        public DateTime BirthDay { get; set; }

        ///<summary>
        ///性别
        /// </summary>
        public bool Sex { get; set; }

        ///<summary>
        ///学生姓名
        /// </summary>
        [DisplayName("学生姓名")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string StudentName { get; set; }

        ///<summary>
        ///学号
        /// </summary>
        [DisplayName("学号")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string StudentNo { get; set; }

        ///<summary>
        ///班级
        /// </summary>
        [DisplayName("班级")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string Class { get; set; }

        ///<summary>
        ///联系方式
        /// </summary>
        [Phone(ErrorMessage = "请输入有效的{0}！")]
        [DisplayName("联系方式")]
        public string Contact { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        [EmailAddress(ErrorMessage = "请输入有效的{0}地址")]
        public string Email { get; set; }

    }
}
