using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.MODEL;

namespace LibraryManagementSystem.IBLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.IBLL
    * 文件名称  :IFinePaymentManager
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-03-20 12:21:27
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public interface IFinePaymentManager
    {
        Task<bool> CreateFinePayment(FinePayment model);
        Task<bool> UpdateFinePayment(Guid id,UpdateFinePaymentDto model,string fields);
        Task<List<FinePaymentDto>> GetFinePayments(bool includeRemove);
        Task<FinePaymentDto> GetFinePayment(Guid id);
    }
}
