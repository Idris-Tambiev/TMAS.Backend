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

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly Base.UserParams _params;
        private readonly UserManager<User> _userManager;
        public UsersController(UserService service, Base.UserParams param,UserManager<User> userManager)
        {
            _userService = service;
            _params = param;
            _userManager = userManager;
        }

        [HttpPost("/login/user")]
        public async Task<ActionResult<User>> Login (User model)
        {
            return Ok(await _userService.GetOneByEmail(model));
        }

        [HttpPost("/create/user")]
        public async Task<ActionResult<User>> Registrate(RegistrateUserDto model)
        {
            return Ok(await _userService.Create(model));
        }

        [HttpGet("test")]
        [Authorize]
        public Guid Test()
        {
            var id = _params.GetId(HttpContext);
            return id;
        }
    }
}
