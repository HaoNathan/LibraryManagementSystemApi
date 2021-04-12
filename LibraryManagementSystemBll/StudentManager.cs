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
    * 文件名称  :IStudentManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-24 21:28:34 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class StudentManager : IStudentManager
    {
        public StudentManager(IMapper mapper, IStudentService service, IDepartmentService departmentService)
        {
            _mapper = mapper;
            _service = service;
            _departmentService = departmentService;
        }

        private readonly IMapper _mapper;

        private readonly IStudentService _service;

        private readonly IDepartmentService _departmentService;

        public async Task<StudentDto> GetStudent(Guid id) => _mapper.Map<StudentDto>(await _service.Query(id, false));
        public async Task<int> GetStudentTotal() => await _service.TotalCount(false);
        public async Task<List<StudentDto>> GetStudents(bool includeRemove) =>
           await GetStudents(false, new Dictionary<string, string>(), includeRemove);

        public async Task<List<StudentDto>> GetStudents(Dictionary<string, string> dic) =>
            await GetStudents(true, dic);

        public async Task<bool> CreateStudent(Student model) => await _service.Add(model);

        public async Task<bool> UpdateStudent(Guid id, UpdateStudentDto model, string fields)
        {
            var student = await _service.Query(id, false);
            student.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, student, fields));
        }

        public async Task<bool> DeleteStudent(Guid id) => await _service.Delete(id);

        /// <summary>
        /// 获取学生信息数据
        /// </summary>
        /// <returns></returns>
        private async Task<List<StudentDto>> GetStudents(bool hasPara, Dictionary<string, string> dic,
            bool includeRemove = true)
        {
            IEnumerable<Student> students;
            if (!hasPara)
            {
                students = await _service.QueryAll(includeRemove);
            }
            else
            {
                students = await _service.QueryAll(dic);
            }

            var departments = await _departmentService.QueryAll(true);
            var studentsDto = students.Join(departments, stu => stu.DepartmentId, department => department.Id,
                (stu, department) => new StudentDto
                {
                    Id = stu.Id,
                    Age = stu.Age,
                    BirthDay = stu.BirthDay,
                    Class = stu.Class,
                    Contact = stu.Contact,
                    CreateTime = stu.CreateTime,
                    DepartmentId = stu.DepartmentId,
                    DepartmentName = department.DepartmentName,
                    Email = stu.Email,
                    IsRemove = stu.IsRemove,
                    StudentName = stu.StudentName,
                    StudentNo = stu.StudentNo
                }).ToList();
            return studentsDto;
        }
    }
}
