using AutoMapper;
using LibraryManagementSystem.DTO.AddTypeDto;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.MODEL;

namespace LibraryManagementSystem.DTO.Profiles
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.DTO.Profiles
    * 文件名称  :MappingProfile.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-17 19:47:57 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region 设置忽略映射

            //.ForMember(dest => dest.Id, opt => opt.Ignore())
            //.ForMember(dest => dest.CreateTime, opt => opt.Ignore())
            //.ForMember(dest => dest.UpdateTime, opt => opt.Ignore())
            //.ForMember(dest => dest.IsRemove, opt => opt.Ignore());

            #endregion

            CreateMap<AddAdminDto, Admin>();

            CreateMap<Admin, AdminDto>();

            CreateMap<Authority, AuthorityDto>();

            CreateMap<AddBookDto, Book>();

            CreateMap<AddBookCategoryDto, BookCategory>();

            CreateMap<AddPublishingHouseDto, PublishingHouse>();

            CreateMap<AddBookInfoDto, BookInfo>();

            CreateMap<AddBookInventoryDto, BookInventory>();

            CreateMap<AddCompanyDepartmentDto, CompanyDepartment>();

            CreateMap<AddDepartmentDto, Departments>();

            CreateMap<AddFinePaymentDto, FinePayment>();

            CreateMap<AddBorrowDto, Borrow>();

            CreateMap<AddStudentDto, Student>();

            CreateMap<AddEmployeeDto, Employee>();

            CreateMap<AddReservationDto, Reservation>();

            CreateMap<Book, BookDto>();

            CreateMap<FinePayment, FinePaymentDto>();

            CreateMap<BookCategory, BookCategoryDto>();

            CreateMap<PublishingHouse, PublishingHouseDto>();

            CreateMap<BookInfo, BookInfoDto>();

            CreateMap<BookInventory, BookInventoryDto>();

            CreateMap<CompanyDepartment, CompanyDepartmentDto>();

            CreateMap<Departments, DepartmentDto>();

            CreateMap<Borrow, BorrowDto>();

            CreateMap<Student, StudentDto>();

            CreateMap<Employee, EmployeeDto>();

            CreateMap<Reservation, ReservationDto>();
        }
    }
}
