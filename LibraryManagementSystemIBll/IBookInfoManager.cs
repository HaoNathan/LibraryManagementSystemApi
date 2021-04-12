using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.MODEL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementSystem.DTO.UpdateTypeDto;

namespace LibraryManagementSystem.IBLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.IBLL
    * 文件名称  :ISystemSettingManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 17:22:34 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public interface IBookInfoManager
    {
        Task<bool> CreateBookInfo(BookInfo model);
        Task<bool> UpdateBookInfo(Guid id,UpdateBookInfoDto model, string field);
        Task<List<BookInfoDto>> GetBookInfos(bool includeRemove);
        Task<List<BookInfoDto>> GetBookInfos(Dictionary<string, string> dic);
        Task<BookInfoDto> GetBookInfo(Guid id);
    }
}
