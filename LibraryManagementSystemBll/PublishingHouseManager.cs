using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.DTO.AddTypeDto;
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
    * 文件名称  :PublishingHouseManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-16 18:19:50 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class PublishingHouseManager : IPublishingHouseManager
    {
        public PublishingHouseManager(IPublishingHouseService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        private readonly IMapper _mapper;

        private readonly IPublishingHouseService _service;

        public async Task<bool> CreatePublishingHouse(PublishingHouse model)
        {
            return await _service.Add(_mapper.Map<PublishingHouse>(model));
        }

        public async Task<bool> UpdatePublishingHouse(Guid id, UpdatePublishingHouseDto model, string fields)
        {
            var publishingHouse = await _service.Query(id, false);
            publishingHouse.UpdateTime = DateTime.Now;
            return await _service.Update(
                CommonClass.SetModelValue(model, publishingHouse, fields));
        }

        public async Task<List<PublishingHouseDto>> GetPublishingHouses(bool includeRemove)
        {
            var data = await _service.QueryAll(includeRemove);
            return _mapper.Map<List<PublishingHouseDto>>(data);
        }

        public async Task<PublishingHouseDto> GetPublishingHouse(Guid id) =>
            _mapper.Map<PublishingHouseDto>(await _service.Query(id, false));
    }
}
