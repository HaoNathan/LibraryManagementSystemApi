using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
    public class BookInfoController : ControllerBase
    {
        public BookInfoController(ILogger<BookInfoController> logger, IBookInfoManager bookInfoManager,
            IBookCategoryManager manager, IMapper mapper,
            IPublishingHouseManager publishingHouseManager)
        {
            _logger = logger;
            _bookCategoryManager = manager;
            _publishingHouseManager = publishingHouseManager;
            _bookInfoManager = bookInfoManager;
            _mapper = mapper;
        }

        private readonly ILogger<BookInfoController> _logger;
        private readonly IBookCategoryManager _bookCategoryManager;
        private readonly IPublishingHouseManager _publishingHouseManager;
        private readonly IBookInfoManager _bookInfoManager;
        private readonly IMapper _mapper;

        [HttpGet]
        [Route("BookCategories")]
        public async Task<IActionResult> BookCategories([FromQuery] bool includeRemove)
        {
            try
            {
                var data = await _bookCategoryManager.GetBookCategories(includeRemove);
                return Ok(new JsonMessageResult("Success", 1, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpGet]
        [Route("PublishingHouses")]
        public async Task<IActionResult> PublishingHouses([FromQuery] bool includeRemove)
        {
            try
            {
                var data = await _publishingHouseManager.GetPublishingHouses(includeRemove);
                return Ok(new JsonMessageResult("Success", 1, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpGet]
        [Route("BooksInfo")]
        public async Task<IActionResult> BooksInfo([FromQuery] bool includeRemove)
        {
            try
            {
                var data = await _bookInfoManager.GetBookInfos(includeRemove);
                return Ok(new JsonMessageResult("Success", 1, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpGet]
        [Route("BooksInfoByParameter")]
        public async Task<IActionResult> BooksInfo([FromQuery] Dictionary<string,string>dic)
        {
            try
            {
                var data = await _bookInfoManager.GetBookInfos(dic);
                return Ok(new JsonMessageResult("Success", 1, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpGet]
        [Route("BookInfo/{id}")]
        public async Task<IActionResult> BookInfo(Guid id)
        {
            try
            {
                var data = await _bookInfoManager.GetBookInfo(id);
                return Ok(new JsonMessageResult("Success", 1, data));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPost]
        [Route("CreateBookCategory")]
        public async Task<IActionResult> CreateBookCategory([FromBody] AddBookCategoryDto model)
        {
            try
            {
                var bookCategory = _mapper.Map<BookCategory>(model);
                var result = await _bookCategoryManager.CreateBookCategory(bookCategory);
                if (!result)
                    return Ok(new JsonMessageResult("Fail", 0, null));
                return CreatedAtAction(nameof(BookCategories), bookCategory);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPost]
        [Route("CreatePublishingHouse")]
        public async Task<IActionResult> CreatePublishingHouse([FromBody] AddPublishingHouseDto model)
        {
            try
            {
                var publishingHouse = _mapper.Map<PublishingHouse>(model);
                var result = await _publishingHouseManager.CreatePublishingHouse(publishingHouse);
                if (!result)
                    return Ok(new JsonMessageResult("Fail", 0, null));
                return CreatedAtAction(nameof(PublishingHouses), publishingHouse);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPost]
        [Route("CreateBookInfo")]
        public async Task<IActionResult> CreateBookInfo([FromBody] AddBookInfoDto model)
        {
            try
            {
                var bookInfo = _mapper.Map<BookInfo>(model);
                var result = await _bookInfoManager.CreateBookInfo(bookInfo);
                if (!result)
                    return Ok(new JsonMessageResult("Fail", 0, null));
                return CreatedAtAction(nameof(BooksInfo), bookInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPut]
        [Route("UpdateBookInfo/{id}")]
        public async Task<IActionResult> UpdateBookInfo(Guid id, [FromBody] UpdateBookInfoDto model, [FromQuery] string fields)
        {
            try
            {
                var result = await _bookInfoManager.UpdateBookInfo(id, model, fields);
                if (!result)
                    return Ok(new JsonMessageResult("Fail", 0, null));
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPut]
        [Route("UpdateBookCategory/{id}")]
        public async Task<IActionResult> UpdateBookCategory(Guid id, [FromBody] UpdateBookCategoryDto model)
        {
            try
            {
                var result = await _bookCategoryManager.UpdateBookCategory(id, model, model.Fields);
                if (!result)
                    return Ok(new JsonMessageResult("Fail", 0, null));
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPut]
        [Route("UpdatePublishingHouse/{id}")]
        public async Task<IActionResult> UpdatePublishingHouse([FromRoute] Guid id, [FromBody] UpdatePublishingHouseDto model)
        {
            try
            {
                var result = await _publishingHouseManager.UpdatePublishingHouse(id, model, model.Fields);
                if (!result)
                    return Ok(new JsonMessageResult("Fail", 0, null));
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
