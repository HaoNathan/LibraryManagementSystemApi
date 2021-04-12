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
    public class DepartmentController : ControllerBase
    {
        public DepartmentController(ILogger<DepartmentController> logger, IMapper mapper, IDepartmentManager manager)
        {
            _logger = logger;
            _mapper = mapper;
            _manager = manager;
        }
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMapper _mapper;
        private readonly IDepartmentManager _manager;

        [HttpGet]
        [Route("GetDepartment/{id}")]
        public async Task<IActionResult> GetDepartment(Guid id)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetDepartment(id)));
        }

        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments([FromQuery] bool includeRemove)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetDepartments(includeRemove)));
        }

        [HttpPost]
        [Route("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment(AddDepartmentDto model)
        {
            try
            {
                var department = _mapper.Map<Departments>(model);
                if (!await _manager.CreateDepartment(department))
                {
                    return Ok(new JsonMessageResult("创建失败！", 0, null));
                }

                return CreatedAtAction(nameof(GetDepartment), new { department.Id }, department);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPut]
        [Route("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] UpdateDepartmentDto model,
            [FromQuery] string fields)
        {
            try
            {
                if (!await _manager.UpdateDepartment(id, model, fields))
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

        [HttpDelete]
        [Route("DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                if (!await _manager.DeleteDepartment(id))
                {
                    return Ok(new JsonMessageResult("创建失败！", 0, null));
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
