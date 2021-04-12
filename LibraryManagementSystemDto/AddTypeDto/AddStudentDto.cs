using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.AddTypeDto
{
    /// <summary>
    /// 学生信息实体类
    /// </summary>
    public class AddStudentDto
    {
        ///<summary>
        ///年龄
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("年龄")]
        [Range(1,100,ErrorMessage = "{0}应介于{1}至{2}")]
        public int Age { get; set; }

        ///<summary>
        ///所在系部
        ///</summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("系部Id")]
        public Guid DepartmentId { get; set; }

        ///<summary>
        ///出生年月
        /// </summary>
        [DisplayName("出生日期")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0}是必须的")]
        public DateTime BirthDay { get; set; }

        ///<summary>
        ///性别
        /// </summary>
        [DisplayName("性别")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0}是必须的")]
        public bool Sex { get; set; }

        ///<summary>
        ///学生姓名
        /// </summary>
        [DisplayName("学生姓名")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0}是必须的")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string StudentName { get; set; }

        ///<summary>
        ///学号
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("学号")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string StudentNo { get; set; }

        ///<summary>
        ///班级
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("班级")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string Class { get; set; }

        ///<summary>
        ///联系方式
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [Phone(ErrorMessage = "请输入有效的{0}！")]
        [DisplayName("联系方式")]
        public string Contact { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("邮箱")]
        [EmailAddress(ErrorMessage = "请输入有效的{0}地址")]
        public string Email { get; set; }

    }
}
