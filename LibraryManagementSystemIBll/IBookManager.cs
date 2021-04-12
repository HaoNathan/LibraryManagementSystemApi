using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.DTO.AddTypeDto;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.MODEL;

namespace LibraryManagementSystem.IBLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.IBLL
    * 文件名称  :IBookManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 15:26:18 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public interface IBookManager
    {
        Task<bool> CreateBook(Book model);
        Task<BookDto> GetBook(Guid id);
        Task<int> GetTotal();
        Task<List<BookDto>> GetBooks(bool includeRemove);
        Task<List<BookDto>> GetBooks(Dictionary<string,string> dicPara);
        Task<bool> UpdateBook(Guid id, UpdateBookDto model, string fields);
        Task<bool> UpdateBookSate(Guid id, int state);
        Task<bool> DeleteBook(Guid id);
    }
}
