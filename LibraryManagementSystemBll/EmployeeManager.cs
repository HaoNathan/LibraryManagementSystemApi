using System;
using System.Collections.Generic;
using System.Linq;
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
    * 文件名称  :IEmployeeManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-24 21:28:06 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class EmployeeManager : IEmployeeManager
    {
        public EmployeeManager(IMapper mapper, IEmployeeService service, ICompanyDepartmentService departmentService)
        {
            _mapper = mapper;
            _service = service;
            _departmentService = departmentService;
        }

        private readonly IMapper _mapper;

        private readonly IEmployeeService _service;
        private readonly ICompanyDepartmentService _departmentService;

        public async Task<EmployeeDto> GetEmployee(Guid id) =>
            _mapper.Map<EmployeeDto>(await _service.Query(id, false));

        public async Task<int> GetTotal() => await _service.TotalCount(false);

        public async Task<List<EmployeeDto>> GetEmployees(bool includeRemove) =>
            await GetEmployees(false, new Dictionary<string, string>(), includeRemove);

        public async Task<bool> CreateEmployee(Employee model) => await _service.Add(model);

        public async Task<bool> UpdateEmployee(Guid id, UpdateEmployeeDto model, string fields)
        {
            var employee = await _service.Query(id, false);
            employee.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, employee, fields));
        }

        public async Task<bool> DeleteEmployee(Guid id) => await _service.Delete(id);

        public async Task<List<EmployeeDto>> GetEmployees(Dictionary<string, string> dic) =>
            await GetEmployees(true, dic);

        private async Task<List<EmployeeDto>> GetEmployees(bool hasPara, Dictionary<string, string> dic,
            bool includeRemove = true)
        {
            IEnumerable<Employee> employees;
            if (!hasPara)
            {
                employees = await _service.QueryAll(includeRemove);
            }
            else
            {
                employees = await _service.QueryAll(dic);
            }

            var departments = await _departmentService.QueryAll(true);
            var employeesDto = employees.Join(departments, employee => employee.DepartmentId,
                department => department.Id,
                (employee, department) => new EmployeeDto
                {
                    Id = employee.Id,
                    Age = employee.Age,
                    BirthDay = employee.BirthDay,
                    Contact = employee.Contact,
                    CreateTime = employee.CreateTime,
                    DepartmentId = employee.DepartmentId,
                    CompanyDepartmentName = department.CompanyDepartmentName,
                    IsRemove = employee.IsRemove,
                    EmployeeName = employee.EmployeeName,
                    Sex = employee.Sex
                }).ToList();
            return employeesDto;
        }
    }
}
