using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.MODEL.CommonModel;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.DTO.AddTypeDto;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.IBLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public AdminController(IAdminManager manager, IConfiguration configuration, ILogger<AdminController> logger)
        {
            _manager = manager;

            _configuration = configuration;

            _logger = logger;
        }

        private readonly IAdminManager _manager;

        private readonly IConfiguration _configuration;

        private readonly ILogger<AdminController> _logger;

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AdminDto model)
        {
            var dic = new Dictionary<string, object>
            {
                {"AdminName", model.AdminName},
                {"AdminPassword", model.AdminPassword}
            };

            if (model.AdminPassword.Equals("FaceRecognition")) dic.Remove("AdminPassword");

            try
            {
                var res = await _manager.IsExist(dic);
                return res
                    ? Ok(new JsonMessageResult(GetJwtToken(model.AdminName), 1, null))
                    : Ok(new JsonMessageResult("身份认证失败！", 0, null));
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
        }

        [HttpPost]
        [Route("CreateAdmin")]
        [Authorize]
        public async Task<IActionResult> CreateAdmin([FromBody] AddAdminDto model)
        {
            var res = await _manager.AddAdmin(model);
            if (!res)
            {
                return UnprocessableEntity();
            }

            return CreatedAtAction(nameof(CreateAdmin), model);
        }

        [Route("AdminInfo")]
        [HttpGet]
        public async Task<IActionResult> AdminInfo([FromQuery] AdminDto model)
        {
            var dic = new Dictionary<string, string>
            {
                {"adminName", model.AdminName},
                {"adminPassword", model.AdminPassword}
            };

            if (model.AdminPassword.Equals("FaceRecognition"))
            {
                dic.Remove("adminPassword");
            }

            if (model.Id != Guid.Empty)
            {
                dic.Add("Id", model.Id.ToString());
            }

            var admin = await _manager.QueryAdmin(dic);
            return Ok(new JsonMessageResult("Success", 1, admin));
        }
        
        [HttpDelete]
        [Route("DeleteAdmin")]
        public async Task<IActionResult> DeleteAdmin([FromQuery]Guid adminId)
        {
            try
            {
                await _manager.RemoveAdmin(adminId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Ok(new JsonMessageResult(e.Message, 0, null));
            }
            return NoContent();
        }

        [HttpPut]
        [Route("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin([FromBody] UpdateAdminDto model, [FromQuery] string fields)
        {
            try
            {
                var res = await _manager.UpdateAdmin(model.Id, model, fields);
                if (!res)
                {
                    return Ok(new JsonMessageResult("修改失败！", 0, 0));
                }

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Ok(new JsonMessageResult("", 0, 0));
            }
        }

        [HttpGet]
        [Route("Authorities")]
        public async Task<IActionResult> Authorities([FromQuery]bool includeRemove)
        {
            var data = await _manager.QueryAllAuthority(includeRemove);
            return Ok(new JsonMessageResult("Success",1,data));
        }

        [HttpGet]
        [Route("GetAdmins")]
        public async Task<IActionResult> GetAdmins([FromQuery]bool includeRemove)
        {
            var data = new List<AdminDto>();
            try
            {
                data = await _manager.QueryAllAdmin(includeRemove);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return Ok(new JsonMessageResult(e.Message, 0, data));
            }

            return Ok(new JsonMessageResult("Success", 1, data));
        }

        [HttpGet]
        [Route("GetAdminsByPara")]
        public async Task<IActionResult> GetAdminsByPara([FromQuery] Dictionary<string,string>dic)
        {
            var data = new List<AdminDto>();
            try
            {
                data = await _manager.QueryAllAdminByPara(dic);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return Ok(new JsonMessageResult(e.Message, 0, data));
            }

            return Ok(new JsonMessageResult("Success", 1, data));
        }

        #region 获取Token

        /// <summary>
        /// 获取授权Token
        /// </summary>
        /// <returns></returns>
        private string GetJwtToken(string adminName)
        {
            //签名算法
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, adminName)
            };

            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:Secretkey"]);
            var signingKey = new SymmetricSecurityKey(secretByte);
            var signingCredentials = new SigningCredentials(signingKey, signingAlgorithm);

            var token = new JwtSecurityToken(_configuration["Authentication:Issuer"], _configuration["Authentication:Audience"],
                claims, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
