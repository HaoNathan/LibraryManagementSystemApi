using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    * 文件名称  :FinePaymentManager
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-03-20 12:28:07
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class FinePaymentManager : IFinePaymentManager
    {
        public FinePaymentManager(IMapper mapper, IFinePaymentService service)
        {
            _mapper = mapper;
            _service = service;
        }

        private readonly IMapper _mapper;
        private readonly IFinePaymentService _service;

        public async Task<bool> CreateFinePayment(FinePayment model) => await _service.Add(model);

        public async Task<bool> UpdateFinePayment(Guid id, UpdateFinePaymentDto model, string fields)
        {
            var finePayment = await _service.Query(id, true);
            finePayment.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, finePayment, fields));
        }

        public async Task<List<FinePaymentDto>> GetFinePayments(bool includeRemove) =>
            _mapper.Map<List<FinePaymentDto>>(await _service.QueryAll(includeRemove));

        public async Task<FinePaymentDto> GetFinePayment(Guid id) =>
            _mapper.Map<FinePaymentDto>(await _service.Query(id, true));
    }
}
