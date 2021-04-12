using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.DTO.AddTypeDto;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.IBLL;
using LibraryManagementSystem.MODEL;
using LibraryManagementSystem.MODEL.CommonModel;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        public BorrowController(ILogger<BorrowController> logger, IMapper mapper, IBorrowManager manager)
        {
            _logger = logger;
            _mapper = mapper;
            _manager = manager;
        }

        private readonly ILogger<BorrowController> _logger;
        private readonly IMapper _mapper;
        private readonly IBorrowManager _manager;

        [HttpGet]
        [Route("GetBorrow/{id}")]
        public async Task<IActionResult> GetBorrow(Guid id)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetBorrow(id)));
        }

        [HttpGet]
        [Route("GetBorrows")]
        public async Task<IActionResult> GetBorrows([FromQuery] bool includeRemove)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetBorrows(includeRemove)));
        }

        [HttpGet]
        [Route("GetBorrowsTotal")]
        public async Task<IActionResult> GetBorrowsTotal()
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetTotal()));
        }

        [HttpGet]
        [Route("GetBorrowInfosTotal")]
        public async Task<IActionResult> GetBorrowInfosTotal(Guid studentId)
        {
            try
            {
                return Ok(new JsonMessageResult("Success", 1, await _manager.GetInfosTotal(studentId)));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Ok(new JsonMessageResult(e.Message, 0, -1));
            }
        }

        [HttpGet]
        [Route("GetBorrowsByParameter")]
        public async Task<IActionResult> GetBorrows([FromQuery] Dictionary<string, string> dicPara)
        {
            try
            {
                if (dicPara.Keys.Contains("SearchPara"))
                {
                    var borrows = await _manager.GetBorrows(true);
                    var beginDate = Convert.ToDateTime(dicPara["BeginDate"]);
                    var EndDate = Convert.ToDateTime(dicPara["EndDate"]);
                    var para = dicPara["SearchPara"];
                    var data = borrows.Where(m =>
                        m.CreateTime >= beginDate && m.CreateTime <= EndDate && m.StudentName.Contains(para) ||
                        m.StudentNo.Contains(para)).ToList();
                    return Ok(new JsonMessageResult("Success", 1, data));
                }

                var book = await _manager.GetBorrows(dicPara);
                return Ok(new JsonMessageResult("Success", 1, book));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Ok(new JsonMessageResult(e.Message, 0, new List<BorrowDto>()));
            }
        }

        [HttpPost]
        [Route("CreateBorrow")]
        public async Task<IActionResult> CreateBorrow([FromBody] List<AddBorrowDto> borrowInfos)
        {
            try
            {
                var borrows = _mapper.Map<List<Borrow>>(borrowInfos);
                await _manager.CreateBorrow(borrows);
                return CreatedAtAction(nameof(GetBorrows), new { includeRemove = true }, borrows);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, 0));
            }
        }

        [HttpPut]
        [Route("ReturnBook/{id}")]
        public async Task<IActionResult> UpdateBorrow(Guid id)
        {
            try
            {
                var res = await _manager.ReturnBook(id);
                if (!res)
                {
                    return Ok(new JsonMessageResult("归还失败!", 0, 0));
                }
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, 0));
            }
        }

        [HttpPut]
        [Route("UpdateBorrow/{id}")]
        public async Task<IActionResult> UpdateBorrow(Guid id, [FromBody] UpdateBorrowDto model,
            [FromQuery] string fields)
        {
            try
            {
                if (!await _manager.UpdateBorrow(id, model, fields))
                {
                    return Ok(new JsonMessageResult("修改失败！", 0, 0));
                }

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, 0));
            }
        }

        [HttpDelete]
        [Route("DeleteBorrow/{id}")]
        public async Task<IActionResult> DeleteBorrow(Guid id)
        {
            try
            {
                if (!await _manager.DeleteBorrow(id))
                {
                    return Ok(new JsonMessageResult("删除失败！", 0, 0));
                }

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, 0));
            }
        }

    }
}
