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
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public StudentController(IStudentManager manager, ILogger<StudentController> logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentManager _manager;

        [HttpGet]
        [Route("GetStudent/{id}")]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetStudent(id)));
        }

        [HttpGet]
        [Route("GetStudents")]
        public async Task<IActionResult> GetStudents([FromQuery] bool includeRemove)
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetStudents(includeRemove)));
        }

        [HttpGet]
        [Route("GetStudentsTotal")]
        public async Task<IActionResult> GetStudentsTotal()
        {
            return Ok(new JsonMessageResult("Success", 1, await _manager.GetStudentTotal()));
        }

        [HttpGet]
        [Route("GetStudentsByParameter")]
        public async Task<IActionResult> GetStudents([FromQuery] Dictionary<string, string> dic)
        {
            try
            {
               return Ok(new JsonMessageResult("Success", 1, await _manager.GetStudents(dic)));
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, new List<AddStudentDto>()));
            }
        }

        [HttpPost]
        [Route("CreateStudent")]
        public async Task<IActionResult> CreateStudent(AddStudentDto model)
        {
            try
            {
                var student = _mapper.Map<Student>(model);
                if (!await _manager.CreateStudent(student))
                {
                    return Ok(new JsonMessageResult("创建失败！", 0, null));
                }

                return CreatedAtAction(nameof(GetStudent), new { student.Id }, student);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPut]
        [Route("UpdateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentDto model,
            [FromQuery] string fields)
        {
            try
            {
                if (!await _manager.UpdateStudent(id, model, fields))
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
        [Route("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                if (!await _manager.DeleteStudent(id))
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