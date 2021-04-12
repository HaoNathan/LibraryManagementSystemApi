using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.AddTypeDto
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.DTO.AddTypeDto
    * 文件名称  :AddPublishingHouseDto.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 16:10:23 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class AddPublishingHouseDto
    {
        ///<summary>
        ///出版社名称
        /// </summary>
        [Required(ErrorMessage = "{0}是必须的！")]
        [DisplayName("出版社名称")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "{0}的长度范围应该是{2}到{1}")]
        public string PublishingName { get; set; }
    }
}
