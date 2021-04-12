using System;

namespace LibraryManagementSystem.DTO.UpdateTypeDto
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.DTO.UpdateTypeDto
    * 文件名称  :UpdateBookDto.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 17:32:35 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class UpdateBookDto
    {
        /// <summary>
        ///是否移除
        /// </summary>
        public bool IsRemove { get; set; }

        ///<summary>
        ///书籍详细信息
        /// </summary>
        public Guid BookInfoId { get; set; }

        ///<summary>
        ///书籍状态
        /// </summary>
        public int BookState { get; set; }
    }
}
