using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.MODEL;

namespace LibraryManagementSystem.IBLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.IBLL
    * 文件名称  :IReservationManager
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-03-11 15:13:19
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public interface IReservationManager
    {
        Task CreateReservation(List<Reservation> reservations);
        Task<ReservationDto> GetReservation(Guid id);
        Task<int> GetTotal();
        Task<List<Guid>> GetReservationBooks();
        Task<List<ReservationDto>> GetReservations(bool includeRemove);
        Task<List<ReservationDto>> GetReservations(Dictionary<string, string> dic);
        Task<bool> UpdateReservation(Guid id, UpdateReservationDto model, string fields);
        Task<bool> DeleteReservation(Guid id);
        Task<bool> UpdateState(Guid id, bool isRemove);
        Task<int> GetInfosTotal(Guid studentId);
    }
}
