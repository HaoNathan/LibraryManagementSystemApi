using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.DAL;
using LibraryManagementSystem.DTO.ChartDto;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.IBLL;
using LibraryManagementSystem.IDAL;

namespace LibraryManagementSystem.BLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.BLL
    * 文件名称  :ChartManager
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-03-28 09:43:58
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class ChartManager : IChartManager
    {

        private readonly IBookService _service;

        public ChartManager(IBookService service)
        {
            _service = service;
        }

        public async Task<List<BookChartDto>> BooksSummaryByCategory()
        {
            return await _service.RunProc<BookChartDto>("BookSummaryByCategoryChart", new Dictionary<string, string>());
        }

        public async Task<List<BorrowChartDto>> BorrowsSummaryByDay()
        {
            return await _service.RunProc<BorrowChartDto>("BorrowSummaryByDayChart", new Dictionary<string, string>());
        }
    }
}
