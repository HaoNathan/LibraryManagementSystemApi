using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.AddTypeDto
{
    /// <summary>
    /// 系部实体类
    /// </summary>
    public class AddDepartmentDto
    {
        ///<summary>
        ///系部名称
        /// </summary>
        [DisplayName("系部名称")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0}是必须的")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string DepartmentName { get; set; }
    }
}