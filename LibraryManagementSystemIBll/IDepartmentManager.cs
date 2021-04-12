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
    * 文件名称  :IDepartmentManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-24 21:31:38 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public interface IDepartmentManager
    {
        Task<DepartmentDto> GetDepartment(Guid id);
        Task<List<DepartmentDto>> GetDepartments(bool includeRemove);
        Task<bool> CreateDepartment(Departments model);
        Task<bool> UpdateDepartment(Guid id, UpdateDepartmentDto model, string fields);
        Task<bool> DeleteDepartment(Guid id);
    }
}
