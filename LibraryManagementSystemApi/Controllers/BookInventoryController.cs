using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.DTO.AddTypeDto;
using LibraryManagementSystem.IBLL;
using LibraryManagementSystem.MODEL;
using LibraryManagementSystem.MODEL.CommonModel;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookInventoryController : ControllerBase
    {
        public BookInventoryController(ILogger<BookInventoryController> logger, IMapper mapper,
            IBookInventoryManager manager)
        {
            _logger = logger;
            _mapper = mapper;
            _manager = manager;
        }

        private readonly IMapper _mapper;
        private readonly ILogger<BookInventoryController> _logger;
        private readonly IBookInventoryManager _manager;

        [HttpPost]
        [Route("CreateBookInventory")]
        public async Task<IActionResult> CreateBookInventory([FromBody] AddBookInventoryDto model)
        {
            try
            {
                var bookInfo = _mapper.Map<BookInventory>(model);
                var result = await _manager.CreateBookInventory(bookInfo);
                if (!result)
                    return Ok(new JsonMessageResult("Fail", 0, null));
                return CreatedAtAction(nameof(GetBookInventory), new {bookInfo.Id}, bookInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpGet]
        [Route("GetBookInventory/{id}")]
        public async Task<IActionResult> GetBookInventory(Guid id)
        {
            var inventory = await _manager.GetBookInventory(id);
            return Ok(new JsonMessageResult("Success", 1, inventory));
        }

        [HttpGet]
        [Route("GetBookInventories")]
        public async Task<IActionResult> GetBookInventories([FromQuery] bool includeRemove)
        {
            var inventory = await _manager.GetBookInventories(includeRemove);
            return Ok(new JsonMessageResult("Success", 1, 0));
        }
    }
}
