using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DTO.UpdateTypeDto
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.DTO.UpdateTypeDto
    * 文件名称  :UpdateCompanyDepartmentDto.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-24 22:05:05 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class UpdateCompanyDepartmentDto
    {
        ///<summary>
        ///部门名称
        /// </summary>
        [DisplayName("部门名称")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string CompanyDepartmentName { get; set; }
    }
}
