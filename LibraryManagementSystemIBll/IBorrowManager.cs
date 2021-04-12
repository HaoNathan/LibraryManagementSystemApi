using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.MODEL;

namespace LibraryManagementSystem.IBLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.IBLL
    * 文件名称  :IBorrowManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-24 21:30:24 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public interface IBorrowManager
    {
        Task<BorrowDto> GetBorrow(Guid id);
        Task<int> GetTotal();
        Task<List<BorrowDto>> GetBorrows(bool includeRemove);
        Task<List<BorrowDto>> GetBorrows(Dictionary<string, string> dic);
        Task CreateBorrow(List<Borrow> borrows);
        Task<bool> UpdateBorrow(Guid id, UpdateBorrowDto model, string fields);
        Task<bool> DeleteBorrow(Guid id);
        Task<int> GetInfosTotal(Guid studentId);
        Task<bool> ReturnBook(Guid id);
    }
}
