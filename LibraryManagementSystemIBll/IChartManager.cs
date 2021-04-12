using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementSystem.DTO.ChartDto;
using LibraryManagementSystem.DTO.QueryTypeDto;

namespace LibraryManagementSystem.IBLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.IBLL
    * 文件名称  :IChartManager
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-03-28 09:43:39
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public interface IChartManager
    {
        Task<List<BookChartDto>> BooksSummaryByCategory();
        Task<List<BorrowChartDto>> BorrowsSummaryByDay();
    }
}
