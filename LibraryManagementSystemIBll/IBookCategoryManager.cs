using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.MODEL;

namespace LibraryManagementSystem.IBLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.IBLL
    * 文件名称  :IBookCategoryManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 18:05:30 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public interface IBookCategoryManager
    {
        /// <summary>
        /// 创建书籍类别信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> CreateBookCategory(BookCategory model);

        /// <summary>
        /// 修改书籍类别信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="model">需要的修改资源</param>
        /// <param name="field">哪些属性需要修改</param>
        /// <returns></returns>
        Task<bool> UpdateBookCategory(Guid id, UpdateBookCategoryDto model, string field);

        /// <summary>
        /// 获取所有书籍类别信息
        /// </summary>
        /// <param name="includeRemove">是否包含已移除资源</param>
        /// <returns></returns>
        Task<List<BookCategoryDto>> GetBookCategories(bool includeRemove);
    }
}
