using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;
using TMAS.DB.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TMAS.Controllers.Base;
using System.Security.Claims;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly UserService _userService;
        public UsersController(UserService service)
        {
            _userService = service;
        }

        [HttpPost("create")]
        public async Task<ActionResult<User>> Registrate(RegistrateUserDto model)
        {
            return Ok(await _userService.Create(model));
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUserName()
        {
            var id = GetUserId();
            return await _userService.GetOneById(id);
        }

        // api/users/confirmemail
        [HttpGet("confirmemail")]
        public async Task<ActionResult> ConfirmEmail(string userId,string token)
        {
            //var id = GetUserId();
            if(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest();
        }
    }
}
