using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.DTO.AddTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.IBLL;
using LibraryManagementSystem.MODEL;
using LibraryManagementSystem.MODEL.CommonModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(ILogger<EmployeeController> logger, IMapper mapper, IEmployeeManager manager)
        {
            _logger = logger;
            _mapper = mapper;
            _manager = manager;
        }

        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeeManager _manager;

        [HttpGet]
        [Route("GetEmployee/{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetEmployee(id)));
        }

        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees([FromQuery] bool includeRemove)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetEmployees(includeRemove)));
        }

        [HttpGet]
        [Route("GetEmployeesTotal")]
        public async Task<IActionResult> GetEmployeesTotal()
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetTotal()));
        }

        [HttpGet]
        [Route("GetEmployeesByParameter")]
        public async Task<IActionResult> GetEmployees([FromQuery] Dictionary<string, string> dic)
        {
            try
            {
                return Ok(new JsonMessageResult("Success", 1, await _manager.GetEmployees(dic)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, new List<AddStudentDto>()));
            }
        }

        [HttpPost]
        [Route("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(AddEmployeeDto model)
        {
            try
            {
                var employee = _mapper.Map<Employee>(model);
                if (!await _manager.CreateEmployee(employee))
                {
                    return Ok(new JsonMessageResult("创建失败！", 0, 0));
                }

                return CreatedAtAction(nameof(GetEmployee), new { employee.Id }, employee);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, 0));
            }
        }

        [HttpPut]
        [Route("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto model,
            [FromQuery] string fields)
        {
            try
            {
                if (!await _manager.UpdateEmployee(id, model, fields))
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
        [Route("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                if (!await _manager.DeleteEmployee(id))
                {
                    return Ok(new JsonMessageResult("创建失败！", 0, 0));
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
