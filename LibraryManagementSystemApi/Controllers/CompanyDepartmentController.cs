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
    public class CompanyDepartmentController : ControllerBase
    {
        public CompanyDepartmentController(ICompanyDepartmentManager manager, ILogger<CompanyDepartmentController> logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        private readonly ILogger<CompanyDepartmentController> _logger;
        private readonly IMapper _mapper;
        private readonly ICompanyDepartmentManager _manager;

        [HttpGet]
        [Route("GetCompanyDepartment/{id}")]
        public async Task<IActionResult> GetCompanyDepartment(Guid id)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetCompanyDepartment(id)));
        }

        [HttpGet]
        [Route("GetCompanyDepartments")]
        public async Task<IActionResult> GetCompanyDepartments([FromQuery] bool includeRemove)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetCompanyDepartments(includeRemove)));
        }

        [HttpPost]
        [Route("CreateCompanyDepartment")]
        public async Task<IActionResult> CreateCompanyDepartment(AddCompanyDepartmentDto model)
        {
            try
            {
                var companyDepartment = _mapper.Map<CompanyDepartment>(model);
                if (!await _manager.CreateCompanyDepartment(companyDepartment))
                {
                    return Ok(new JsonMessageResult("创建失败！", 0, null));
                }

                return CreatedAtAction(nameof(GetCompanyDepartment), new { companyDepartment.Id }, companyDepartment);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPut]
        [Route("UpdateCompanyDepartment/{id}")]
        public async Task<IActionResult> UpdateCompanyDepartment(Guid id, [FromBody] UpdateCompanyDepartmentDto model,
            [FromQuery] string fields)
        {
            try
            {
                if (!await _manager.UpdateCompanyDepartment(id, model, fields))
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
        [Route("DeleteCompanyDepartment/{id}")]
        public async Task<IActionResult> DeleteCompanyDepartment(Guid id)
        {
            try
            {
                if (!await _manager.RemoveCompanyDepartment(id))
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
