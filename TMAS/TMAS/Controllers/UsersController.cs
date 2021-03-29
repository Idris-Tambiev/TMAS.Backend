using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;
using Microsoft.AspNetCore.Authorization;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _user;
        public UsersController(UserService service)
        {
            _user = service;
        }

        [HttpPost("/login/user")]
        public async Task<ActionResult<User>> Login (User model)
        {
            return Ok(await _user.GetOneByEmail(model));
        }

        [HttpPost("/create/user")]
        public async Task<ActionResult<User>> Registrate( User model)
        {

            return Ok(await _user.Create(model));
        }

        [HttpGet("test")]
        [Authorize]
        public async Task<string> Test()
        {
            var a = HttpContext.User;
            
            return "test successfull";
        }
    }
}
