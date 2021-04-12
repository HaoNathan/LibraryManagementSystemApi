using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    public class BookController : ControllerBase
    {
        public BookController(IBookManager manager, IMapper mapper, ILogger<BookController> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        private readonly IMapper _mapper;
        private readonly IBookManager _manager;
        private readonly ILogger<BookController> _logger;

        [HttpGet]
        [Route("GetBook/{id}")]
        public async Task<IActionResult> GetBook([FromRoute] Guid id)
        {
            var book = await _manager.GetBook(id);
            return Ok(new JsonMessageResult("Success", 1, book));
        }

        [HttpGet]
        [Route("GetBooks")]
        public async Task<IActionResult> GetBooks([FromQuery] bool includeRemove)
        {
            var book = await _manager.GetBooks(includeRemove);
            return Ok(new JsonMessageResult("Success", 1, book));
        }

        [HttpGet]
        [Route("GetBooksTotal")]
        public async Task<IActionResult> GetBooksTotal()
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetTotal()));
        }

        [HttpGet]
        [Route("GetBooksByParameter")]
        public async Task<IActionResult> GetBooks([FromQuery] Dictionary<string, string> dicPara)
        {
            var book = await _manager.GetBooks(dicPara);
            return Ok(new JsonMessageResult("Success", 1, book));
        }

        [HttpPost]
        [Route("CreateBook")]
        public async Task<IActionResult> CreateBook([FromBody] AddBookDto model)
        {
            var book = _mapper.Map<Book>(model);

            try
            {
                if (!await _manager.CreateBook(book))
                    return Ok(new JsonMessageResult("Fail", 0, null));

                return CreatedAtAction(nameof(GetBook), new {book.Id}, book);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult("Fail", 0, null));
            }
        }

        [HttpPut]
        [Route("UpdateBookState")]
        public async Task<IActionResult> UpdateBookState([FromRoute] Guid id, [FromBody] int state)
        {
            try
            {
                var result = await _manager.UpdateBookSate(id, state);
                if (!result)
                {
                    return Ok(new JsonMessageResult("修改书籍状态失败", 0, 0));
                }

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Ok(new JsonMessageResult(e.Message, 0, 0));
            }
        }
    }
}
