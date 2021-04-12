using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementSystem.DTO.ChartDto;
using LibraryManagementSystem.IBLL;
using LibraryManagementSystem.MODEL.CommonModel;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        public ChartController(IChartManager manager, ILogger<ChartController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        private readonly IChartManager _manager;
        private readonly ILogger<ChartController> _logger;

        [HttpGet]
        [Route("BooksSummaryByCategory")]
        public async Task<IActionResult> BooksSummaryByCategory()
        {
            try
            {
                var data =await _manager.BooksSummaryByCategory();
                return Ok(new JsonMessageResult("Success", 1, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,e);
                return Ok(new JsonMessageResult(e.Message, 0, new List<BookChartDto>()));
            }
        }

        [HttpGet]
        [Route("BorrowSummaryByDayChart")]
        public async Task<IActionResult> BorrowSummaryByDayChart()
        {
            try
            {
                var data = await _manager.BorrowsSummaryByDay();
                return Ok(new JsonMessageResult("Success", 1, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Ok(new JsonMessageResult(e.Message, 0, new List<BookChartDto>()));
            }
        }
    }
}
