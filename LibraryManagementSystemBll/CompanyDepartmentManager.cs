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
    * 文件名称  :ICompanyDepartmentManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-24 21:31:03 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class CompanyDepartmentManager : ICompanyDepartmentManager
    {
        public CompanyDepartmentManager(ICompanyDepartmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        private readonly ICompanyDepartmentService _service;

        private readonly IMapper _mapper;

        public async Task<bool> CreateCompanyDepartment(CompanyDepartment model) => await _service.Add(model);

        public async Task<CompanyDepartmentDto> GetCompanyDepartment(Guid id) =>
            _mapper.Map<CompanyDepartmentDto>(await _service.Query(id,false));

        public async Task<List<CompanyDepartmentDto>> GetCompanyDepartments(bool includeRemove) =>
            _mapper.Map<List<CompanyDepartmentDto>>(await _service.QueryAll(includeRemove));

        public async Task<bool> UpdateCompanyDepartment(Guid id, UpdateCompanyDepartmentDto model, string fields)
        {
            var companyDepartment = await _service.Query(id,false);
            companyDepartment.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, companyDepartment, fields));
        }

        public async Task<bool> RemoveCompanyDepartment(Guid id) => await _service.Delete(id);
    }
}
