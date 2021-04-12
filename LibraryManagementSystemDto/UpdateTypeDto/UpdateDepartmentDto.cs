using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.UpdateTypeDto
{
    /// <summary>
    /// 系部实体类
    /// </summary>
    public class UpdateDepartmentDto
    {
        ///<summary>
        ///系部名称
        /// </summary>
        [DisplayName("系部名称")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string DepartmentName { get; set; }
    }
}