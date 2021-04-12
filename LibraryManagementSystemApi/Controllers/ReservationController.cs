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
    public class ReservationController : ControllerBase
    {
        public ReservationController(IMapper mapper,
            ILogger<ReservationController> logger,
            IReservationManager manager)
        {
            _mapper = mapper;
            _logger = logger;
            _manager = manager;
        }

        private readonly IMapper _mapper;
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationManager _manager;

        [HttpGet]
        [Route("GetReservation/{id}")]
        public async Task<IActionResult> GetReservation(Guid id)
        {
            var reservation = await _manager.GetReservation(id);
            return Ok(new JsonMessageResult("Success", 1, reservation));
        }

        [HttpGet]
        [Route("GetReservationsTotal")]
        public async Task<IActionResult> GetReservationsTotal()
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetTotal()));
        }

        [HttpGet]
        [Route("GetReservations")]
        public async Task<IActionResult> GetReservations(bool includeRemove)
        {
            var reservations = await _manager.GetReservations(includeRemove);
            return Ok(new JsonMessageResult("Success", 1, reservations));
        }

        [HttpGet]
        [Route("GetReservationInfosTotal")]
        public async Task<IActionResult> GetReservationInfosTotal(Guid studentId)
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
        [Route("GetReservationsByParameter")]
        public async Task<IActionResult> GetReservations([FromQuery] Dictionary<string, string> dicPara)
        {
            try
            {
                if (dicPara.Keys.Contains("SearchPara"))
                {
                    var reservations = await _manager.GetReservations(true);
                    var beginDate = Convert.ToDateTime(dicPara["BeginDate"]);
                    var EndDate = Convert.ToDateTime(dicPara["EndDate"]);
                    var para = dicPara["SearchPara"];
                    var data = reservations.Where(m =>
                        m.CreateTime >= beginDate && m.CreateTime <= EndDate && m.StudentName.Contains(para) ||
                        m.StudentNo.Contains(para)).ToList();
                    return Ok(new JsonMessageResult("Success", 1, data));
                }

                var book = await _manager.GetReservations(dicPara);
                return Ok(new JsonMessageResult("Success", 1, book));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Ok(new JsonMessageResult(e.Message, 0, new List<BorrowDto>()));
            }
        }


        [HttpGet]
        [Route("GetReservationBooks")]
        public async Task<IActionResult> GetReservationBooks()
        {
            try
            {
                var data = await _manager.GetReservationBooks();
                return Ok(new JsonMessageResult("Success", 1, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Ok(new JsonMessageResult(e.Message, 0, new List<Guid>()));
            }
        }

        [HttpPost]
        [Route("CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] List<AddReservationDto> reservations)
        {
            try
            {
                var reservationInfos = _mapper.Map<List<Reservation>>(reservations);
                await _manager.CreateReservation(reservationInfos);
                return CreatedAtAction(nameof(GetReservations), new { includeRemove = true}, reservationInfos);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, 0));
            }
        }
        [HttpPut]
        [Route("UpdateReservation/{id}")]
        public async Task<IActionResult> UpdateReservation([FromRoute] Guid id, [FromBody] UpdateReservationDto model, [FromQuery] string fields)
        {
            try
            {
                var result = await _manager.UpdateReservation(id, model, fields);
                if (!result) return Ok(new JsonMessageResult("Fail", 0, 0));
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, 0));
            }
        }

        [HttpDelete]
        [Route("DeleteReservation/{id}")]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            try
            {
                var result = await _manager.DeleteReservation(id);
                if (!result) return Ok(new JsonMessageResult("删除失败！", 0, 0));
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
