using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.IBLL;
using LibraryManagementSystem.IDAL;
using LibraryManagementSystem.MODEL;
using LibraryManagementSystemCommon;

namespace LibraryManagementSystem.BLL
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
    public class DepartmentManager : IDepartmentManager
    {
        public DepartmentManager(IDepartmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        private readonly IDepartmentService _service;

        private readonly IMapper _mapper;

        public async Task<DepartmentDto> GetDepartment(Guid id) =>
            _mapper.Map<DepartmentDto>(await _service.Query(id, false));

        public async Task<List<DepartmentDto>> GetDepartments(bool includeRemove) =>
            _mapper.Map<List<DepartmentDto>>(await _service.QueryAll(includeRemove));

        public async Task<bool> CreateDepartment(Departments model) => await _service.Add(model);

        public async Task<bool> UpdateDepartment(Guid id, UpdateDepartmentDto model, string fields)
        {
            var department = await _service.Query(id, false);
            department.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, department, fields));
        }

        public async Task<bool> DeleteDepartment(Guid id) => await _service.Delete(id);
    }
}
