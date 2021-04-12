using LibraryManagementSystem.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.DTO;
using LibraryManagementSystem.DTO.AddTypeDto;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;

namespace LibraryManagementSystem.IBLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.IBLL
    * 文件名称  :IAdminManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-17 17:50:04 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public interface IAdminManager
    {
        Task<bool> AddAdmin(AddAdminDto model);
        Task<bool> UpdateAdmin(Guid id, UpdateAdminDto model, string field);
        Task<bool> RemoveAdmin(Guid id);
        Task<AdminDto> QueryAdmin(Guid id);
        Task<AdminDto> QueryAdmin(Dictionary<string, string> queryParameters);
        Task<List<AdminDto>> QueryAllAdmin(bool includeRemove);
        Task<List<AdminDto>> QueryAllAdminByPara(Dictionary<string,string>dic);
        Task<bool> IsExist(Dictionary<string, object> dic);
        Task<bool> IsExist(Guid id);
        Task<List<AuthorityDto>> QueryAllAuthority(bool includeRemove);
    }
}
