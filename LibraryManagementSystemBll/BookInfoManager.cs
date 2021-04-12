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
    * 命名空间  :LibraryManagementSystem.BLL
    * 文件名称  :BookInfoManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 18:17:10 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class BookInfoManager : IBookInfoManager
    {
        public BookInfoManager(IBookInfoService service, IMapper mapper,
            IBookCategoryService categoryService,
            IPublishingHouseService publishingHouseService
        )
        {
            _service = service;
            _categoryService = categoryService;
            _publishingHouseService = publishingHouseService;
            _mapper = mapper;
        }

        private readonly IBookInfoService _service;

        private readonly IBookCategoryService _categoryService;

        private readonly IPublishingHouseService _publishingHouseService;

        private readonly IMapper _mapper;

        public async Task<bool> CreateBookInfo(BookInfo model)
        {
            return await _service.Add(model);
        }

        public async Task<bool> UpdateBookInfo(Guid id, UpdateBookInfoDto model, string fields)
        {
            var bookInfo = await _service.Query(id, false);
            bookInfo.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, bookInfo, fields));
        }

        public async Task<List<BookInfoDto>> GetBookInfos(bool includeRemove) =>
            await GetBookInfos(1, new Dictionary<string, string>(), includeRemove);

        public async Task<List<BookInfoDto>> GetBookInfos(Dictionary<string, string> dic) =>
            await GetBookInfos(2, dic);

        public async Task<BookInfoDto> GetBookInfo(Guid id)
        {
            return _mapper.Map<BookInfoDto>(await _service.Query(id, false));
        }

        private async Task<List<BookInfoDto>> GetBookInfos(int methodType, Dictionary<string, string> dic,
            bool includeRemove = true)
        {
            IEnumerable<BookInfo> bookInfos;

            if (methodType == 1)
            {
                bookInfos = await _service.QueryAll(includeRemove);
            }
            else
            {
                bookInfos = await _service.QueryAll(dic);
            }

            var categories = await _categoryService.QueryAll(false);
            var publishingHouses = await _publishingHouseService.QueryAll(false);
            var data = bookInfos.Join(categories, bookInfo => bookInfo.BookCategoryId, category => category.Id,
                (bookInfo, category) => new { bookInfo, category }).Join(publishingHouses, ac => ac.bookInfo.PublishingId,
                publishingHouse => publishingHouse.Id, (a, c) => new BookInfoDto
                {
                    Id = a.bookInfo.Id,
                    Author = a.bookInfo.Author,
                    BookCategoryId = a.bookInfo.BookCategoryId,
                    BookName = a.bookInfo.BookName,
                    BookPhoto = a.bookInfo.BookPhoto,
                    BookNum = a.bookInfo.BookNum,
                    CategoryName = a.category.CategoryName,
                    CreateTime = a.bookInfo.CreateTime,
                    ISBN = a.bookInfo.ISBN,
                    IsRemove = a.bookInfo.IsRemove,
                    PublishingName = c.PublishingName,
                    PublishingId = a.bookInfo.PublishingId,
                    UpdateTime = a.bookInfo.UpdateTime,
                    Price = a.bookInfo.Price,
                    ReleaseDate = a.bookInfo.ReleaseDate
                });
            return data.ToList();
        }
    }
}
