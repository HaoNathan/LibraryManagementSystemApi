using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.DTO.AddTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.IBLL;
using LibraryManagementSystem.MODEL;
using LibraryManagementSystem.MODEL.CommonModel;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinePaymentController : ControllerBase
    {
        public FinePaymentController(IFinePaymentManager manager, ILogger<FinePaymentController> logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        private readonly ILogger<FinePaymentController> _logger;
        private readonly IFinePaymentManager _manager;
        private readonly IMapper _mapper;

        [HttpGet]
        [Route("GetFinePayment/{id}")]
        public async Task<IActionResult> GetFinePayment(Guid id)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetFinePayment(id)));
        }

        [HttpGet]
        [Route("GetFinePayments")]
        public async Task<IActionResult> GetFinePayments([FromQuery] bool includeRemove)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetFinePayments(includeRemove)));
        }

        [HttpPost]
        [Route("CreateFinePayment")]
        public async Task<IActionResult> CreateFinePayment(AddFinePaymentDto model)
        {
            try
            {
                var finePayment = _mapper.Map<FinePayment>(model);
                if (!await _manager.CreateFinePayment(finePayment))
                {
                    return Ok(new JsonMessageResult("创建失败！", 0, null));
                }

                return CreatedAtAction(nameof(GetFinePayment), new { finePayment.Id }, finePayment);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPut]
        [Route("UpdateFinePayment/{id}")]
        public async Task<IActionResult> UpdateFinePayment(Guid id, [FromBody] UpdateFinePaymentDto model,
            [FromQuery] string fields)
        {
            try
            {
                if (!await _manager.UpdateFinePayment(id, model, fields))
                {
                    return Ok(new JsonMessageResult("修改失败！", 0, null));
                }

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

    }
}
