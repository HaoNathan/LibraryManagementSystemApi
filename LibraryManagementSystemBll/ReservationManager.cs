using AutoMapper;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.IBLL;
using LibraryManagementSystem.IDAL;
using LibraryManagementSystem.MODEL;
using LibraryManagementSystemCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.BLL
    * 文件名称  :ReservationManager
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-03-11 15:19:17
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class ReservationManager : IReservationManager
    {
        public ReservationManager(IMapper mapper, IReservationService service, IDepartmentService departmentService,
            IAdminService adminService, IStudentService studentService, IBookService bookService)
        {
            _mapper = mapper;
            _service = service;
            _departmentService = departmentService;
            _adminService = adminService;
            _studentService = studentService;
            _bookService = bookService;
        }

        private readonly IMapper _mapper;
        private readonly IReservationService _service;
        private readonly IDepartmentService _departmentService;
        private readonly IAdminService _adminService;
        private readonly IStudentService _studentService;
        private readonly IBookService _bookService;

        public async Task CreateReservation(List<Reservation> reservations)
        {
            foreach (var reservation in reservations) await _service.Add(reservation);
        }

        public async Task<int> GetTotal() => await _service.TotalCount(true);

        public async Task<List<Guid>> GetReservationBooks()
        {
            var reservationInfos = await _service.QueryAll("BookId", false);
            var lists = reservationInfos.TableToList<Reservation>();
            return lists.Select(m => m.BookId).ToList();
        }

        public async Task<ReservationDto> GetReservation(Guid id) =>
            _mapper.Map<ReservationDto>(await _service.Query(id, true));

        public async Task<List<ReservationDto>> GetReservations(bool includeRemove) =>
            await GetReservations(1, new Dictionary<string, string>(), includeRemove);

        public async Task<List<ReservationDto>> GetReservations(Dictionary<string, string> dic) =>
            await GetReservations(2, dic);

        public async Task<bool> UpdateReservation(Guid id, UpdateReservationDto model, string fields)
        {
            var reservation = await _service.Query(id, true);
            reservation.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, reservation, fields));
        }

        public async Task<bool> DeleteReservation(Guid id)
        {
            var reservation = await _service.Query(id, true);
            var book = await _bookService.Query(reservation.BookId, true);
            await _bookService.UpdateSingle(book.Id, "BookState", "0");
            return await _service.Delete(id);
        }

        public async Task<bool> UpdateState(Guid id, bool isRemove)
        {
            var reservation = await _service.Query(id, true);
            reservation.IsRemove = isRemove;
            return await _service.Update(reservation);
        }

        public async Task<int> GetInfosTotal(Guid studentId)
        {
            var data = await _service.QueryAll(new Dictionary<string, string>
            {
                {"StudentId", studentId.ToString()},
                {"IsRemove", false.ToString()}
            });
            var reservations = data.ToList();
            return reservations.Count;
        }

        private async Task<List<ReservationDto>> GetReservations(int methodType, Dictionary<string, string> dic,
            bool includeRemove = true)
        {
            List<Reservation> reservations;
            if (methodType == 1)
            {
                var data = await _service.QueryAll(includeRemove);
                reservations = data.ToList();
            }
            else
            {
                var data = await _service.QueryAll(dic);
                reservations = data.ToList();
            }
            var admins = await _adminService.QueryAll(true);
            var students = await GetStudents();

            return reservations.Join(students, reservation => reservation.StudentId, stu => stu.Id, (reservation, stu) => new { reservation, stu })
                .Join(admins, b => b.reservation.AdminId, admin => admin.Id, (b, admin) => new ReservationDto
                {
                    AdminName = admin.AdminName,
                    StudentName = b.stu.StudentName,
                    StudentNo = b.stu.StudentNo,
                    DepartmentName = b.stu.DepartmentName,
                    Sex = b.stu.Sex,
                    Age = b.stu.Age,
                    Class = b.stu.Class,
                    Contact = b.stu.Contact,
                    Email = b.stu.Email,
                    CreateTime = b.reservation.CreateTime,
                    Id = b.reservation.Id,
                    BookId = b.reservation.BookId,
                    IsRemove = b.reservation.IsRemove
                }).ToList();
        }

        private async Task<List<StudentDto>> GetStudents()
        {
            var students = await _studentService.QueryAll(true);
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
