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
    * 命名空间  :LibraryManagementSystem.BLL
    * 文件名称  :BookCategoryManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 18:18:27 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class BookCategoryManager : IBookCategoryManager
    {
        public BookCategoryManager(IBookCategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        private readonly IMapper _mapper;

        private readonly IBookCategoryService _service;

        public async Task<bool> CreateBookCategory(BookCategory model) =>
            await _service.Add(_mapper.Map<BookCategory>(model));

        public async Task<bool> UpdateBookCategory(Guid id, UpdateBookCategoryDto model, string fields)
        {
            var bookCategory = await _service.Query(id, true);
            bookCategory.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, bookCategory, fields));
        }

        public async Task<List<BookCategoryDto>> GetBookCategories(bool includeRemove)
        {
            var data = await _service.QueryAll(includeRemove);
            return _mapper.Map<List<BookCategoryDto>>(data);
        }
    }
}
