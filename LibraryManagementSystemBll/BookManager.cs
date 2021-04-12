using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.IBLL;
using LibraryManagementSystem.IDAL;
using LibraryManagementSystem.MODEL;
using LibraryManagementSystem.MODEL.CommonModel;
using LibraryManagementSystemCommon;

namespace LibraryManagementSystem.BLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.BLL
    * 文件名称  :BookManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 18:19:14 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class BookManager : IBookManager
    {
        public BookManager(IMapper mapper, IBookService service, IBookCategoryService categoryService,
            IPublishingHouseService publishingHouseService, IBookInfoService bookInfoService)
        {
            _service = service;
            _categoryService = categoryService;
            _publishingHouseService = publishingHouseService;
            _bookInfoService = bookInfoService;
        }

        private readonly IBookService _service;
        private readonly IBookCategoryService _categoryService;
        private readonly IPublishingHouseService _publishingHouseService;
        private readonly IBookInfoService _bookInfoService;
        public async Task<bool> CreateBook(Book model) => await _service.Add(model);

        public async Task<BookDto> GetBook(Guid id)
        {
            var books = await GetBooksByParameter(1, new Dictionary<string, string> {{"Id", id.ToString()}});
            return books.First();
        }

        public async Task<int> GetTotal() => await _service.TotalCount(false);

        public async Task<List<BookDto>> GetBooks(bool includeRemove) =>
            await GetBooksByParameter(0, new Dictionary<string, string>(), includeRemove);

        public async Task<List<BookDto>> GetBooks(Dictionary<string, string> dicPara) =>
            await GetBooksByParameter(1, dicPara);

        public async Task<bool> UpdateBook(Guid id, UpdateBookDto model, string fields)
        {
            var book = await _service.Query(id, false);
            book.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, book, fields));
        }

        public async Task<bool> UpdateBookSate(Guid id, int state) => await _service.UpdateSingle(id, "BookState", state.ToString());

        public async Task<bool> DeleteBook(Guid id) => await _service.Delete(id);

        /// <summary>
        /// 查询书籍数据
        /// </summary>
        /// <param name="methodType">查询方法类型 0：默认全查 其它：带参查询</param>
        /// <param name="dic">查询参数</param>
        /// <param name="includeRemove">是否查询已移除数据</param>
        /// <returns></returns>
        private async Task<List<BookDto>> GetBooksByParameter(int methodType, Dictionary<string, string> dic,
            bool includeRemove = true)
        {
            #region 获取相关参数信息

            var bookInfos = await _bookInfoService.QueryAll(false);
            var categories = await _categoryService.QueryAll(false);
            var publishingHouses = await _publishingHouseService.QueryAll(false);

            #endregion

            //联表查询书籍详细信息
            var bookInfosDto = bookInfos.Join(categories, bookInfo => bookInfo.BookCategoryId, category => category.Id,
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
            IEnumerable<Book> books;
            //获取书籍信息
            if (methodType == 0)
            {
                books = await _service.QueryAll(includeRemove);
            }
            else
            {
                books = await _service.QueryAll(dic);
            }

            //联表查询书籍信息
            var data = books
                .Join(bookInfosDto, book => book.BookInfoId, info => info.Id, (book, info) => new BookDto
                {
                    Id = book.Id,
                    Author = info.Author,
                    BookCategoryId = info.BookCategoryId,
                    BookName = info.BookName,
                    BookPhoto = info.BookPhoto,
                    CategoryName = info.CategoryName,
                    CreateTime = book.CreateTime,
                    ISBN = info.ISBN,
                    IsRemove = book.IsRemove,
                    PublishingName = info.PublishingName,
                    PublishingId = info.PublishingId,
                    UpdateTime = book.UpdateTime,
                    Price = info.Price,
                    ReleaseDate = info.ReleaseDate,
                    BookState = book.BookState,
                    BookInfoId = info.Id
                });

            return data.ToList();
        }
    }
}
