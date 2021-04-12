using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.UpdateTypeDto
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.DTO.UpdateTypeDto
    * 文件名称  :UpdateAdminDto.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 14:22:02 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class UpdateAdminDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        ///<summary>
        ///用户名
        /// </summary>
        [DisplayName("管理员名称")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string AdminName { get; set; }

        ///<summary>
        ///密码
        /// </summary>
        [DisplayName("密码")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string AdminPassword { get; set; }

        ///<summary>
        ///权限
        /// </summary>
        [DisplayName("权限")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string AdministratorRights { get; set; }
    }
}