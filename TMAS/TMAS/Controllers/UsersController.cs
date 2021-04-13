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

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login (User model)
        {
            return Ok(await _userService.GetOneByEmail(model));
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
        public IActionResult GoogleLogin()
        {
            return default;
        }
    }
}
