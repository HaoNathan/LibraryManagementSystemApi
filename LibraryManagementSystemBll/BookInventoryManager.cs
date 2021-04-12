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
    * 文件名称  :BookInventoryManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-20 12:39:08 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class BookInventoryManager : IBookInventoryManager
    {
        public BookInventoryManager(IBookInventoryService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        private readonly IBookInventoryService _service;
        private readonly IMapper _mapper;

        public async Task<bool> CreateBookInventory(BookInventory model) => await _service.Add(model);

        public async Task<BookInventoryDto> GetBookInventory(Guid id) =>
            _mapper.Map<BookInventoryDto>(await _service.Query(id, false));

        public async Task<List<BookInventoryDto>> GetBookInventories(bool includeRemove) =>
            _mapper.Map<List<BookInventoryDto>>(await _service.QueryAll(includeRemove));

        public async Task<bool> UpdateBookInventory(Guid id, UpdateBookInfoDto model, string fields)
        {
            var bookInventory = await _service.Query(id, false);
            bookInventory.UpdateTime = DateTime.Now;
            return await _service.Update(
                CommonClass.SetModelValue(model, bookInventory, fields));
        }

        public async Task<bool> DeleteBookInventory(Guid id)
        {
            return await _service.Delete(id);
        }
    }
}
