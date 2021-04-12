using System;
using System.Collections;
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
    public class BorrowManager : IBorrowManager
    {
        public BorrowManager(IBorrowService service, IMapper mapper, IStudentService studentService,
            IDepartmentService departmentService, IAdminService adminService, IBookService bookService, IReservationService reservationService)
        {
            _service = service;
            _mapper = mapper;
            _studentService = studentService;
            _departmentService = departmentService;
            _adminService = adminService;
            _bookService = bookService;
            _reservationService = reservationService;
        }

        private readonly IMapper _mapper;
        private readonly IBorrowService _service;
        private readonly IDepartmentService _departmentService;
        private readonly IAdminService _adminService;
        private readonly IStudentService _studentService;
        private readonly IBookService _bookService;
        private readonly IReservationService _reservationService;

        public async Task<BorrowDto> GetBorrow(Guid id) => _mapper.Map<BorrowDto>(await _service.Query(id, false));
        public async Task<int> GetTotal() => await _service.TotalCount(true);
        public async Task<List<BorrowDto>> GetBorrows(bool includeRemove) =>
            await GetBorrows(1, new Dictionary<string, string>(), includeRemove);

        public async Task<List<BorrowDto>> GetBorrows(Dictionary<string, string> dic) =>
            await GetBorrows(2, dic);

        public async Task CreateBorrow(List<Borrow> borrows)
        {
            foreach (var borrow in borrows)
            {
                await _service.Add(borrow);
                await _bookService.UpdateSingle(borrow.BookId, "BookState", "1");
            }

            var reservations = new List<Reservation>();
            foreach (var borrow in borrows)
            {
                var data = await _reservationService.QueryAll(new Dictionary<string, string>()
                {
                    {"StudentId", borrow.StudentId.ToString()},
                    {"BookId", borrow.BookId.ToString()},
                    {"IsRemove", "0"}
                });
                reservations.AddRange(data);
            }

            foreach (var reservation in reservations)
            {
                await _reservationService.Delete(reservation.Id);
            }
        }

        public async Task<bool> UpdateBorrow(Guid id, UpdateBorrowDto model, string fields)
        {
            var borrow = await _service.Query(id, false);
            borrow.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, borrow, fields));
        }

        public async Task<int> GetInfosTotal(Guid studentId)
        {
            var data = await _service.QueryAll(new Dictionary<string, string>
            {
                {"StudentId", studentId.ToString()},
                {"IsRemove", false.ToString()}
            });
            var borrows = data.ToList();
            if (borrows.Count <= 0) return -1;

            return borrows.Count;
        }

        public async Task<bool> ReturnBook(Guid id)
        {
            var borrow = await _service.Query(id, true);
            borrow.BackTime = DateTime.Now;
            borrow.IsRemove = true;
            var borrowRes = await _service.Update(borrow);
            var reservation = await _reservationService.IsExist(new Dictionary<string, object>
            {
                {"BookId", borrow.BookId}
            });
            var bookRes = await _bookService.UpdateSingle(borrow.BookId, "BookState", reservation > 0 ? "2" : "0");
            return borrowRes && bookRes;
        }

        public async Task<bool> DeleteBorrow(Guid id) => await _service.Delete(id);

        private async Task<List<BorrowDto>> GetBorrows(int methodType, Dictionary<string, string> dic,
            bool includeRemove = true)
        {
            List<Borrow> borrows;
            if (methodType == 1)
            {
                var data = await _service.QueryAll(includeRemove);
                borrows = data.ToList();
            }
            else
            {
                var data = await _service.QueryAll(dic);
                borrows = data.ToList();
            }
            var admins = await _adminService.QueryAll(true);
            var students = await GetStudents();
            
            return borrows.Join(students, borrow => borrow.StudentId, stu => stu.Id, (borrow, stu) => new { borrow, stu })
                .Join(admins, b => b.borrow.AdminId, admin => admin.Id, (b, admin) => new BorrowDto
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
                    EndTime = b.borrow.EndTime,
                    BackTime = b.borrow.BackTime,
                    Fine = b.borrow.EndTime < DateTime.Now ? Convert.ToDecimal((DateTime.Now - b.borrow.EndTime).TotalDays * 0.1) : 0,
                    IsExpired = b.borrow.EndTime < DateTime.Now,
                    CreateTime = b.borrow.CreateTime,
                    Id = b.borrow.Id,
                    IsRemove = b.borrow.IsRemove,
                    BookId = b.borrow.BookId
                }).ToList();
        }

        private async Task<List<StudentDto>> GetStudents()
        {
            var students = await _studentService.QueryAll(true);
            var departments = await _departmentService.QueryAll(true);
            var studentsDto = students.Join(departments, stu => stu.DepartmentId, department => department.Id,
                (stu, department) => new StudentDto()
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
