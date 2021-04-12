using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.AddTypeDto
{
    /// <summary>
    /// 员工部门实体类
    /// </summary>
    public class AddCompanyDepartmentDto
    {
        ///<summary>
        ///部门名称
        /// </summary>
        [DisplayName("部门名称")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0}是必须的")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string CompanyDepartmentName { get; set; }

    }
}