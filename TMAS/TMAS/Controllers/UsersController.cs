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
using Microsoft.AspNetCore.Identity;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _user;
        private readonly Base.UserParams _params;
        private readonly UserManager<User> _userManager;
        public UsersController(UserService service, Base.UserParams param,UserManager<User> userManager)
        {
            _user = service;
            _params = param;
            _userManager = userManager;
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
        public async Task<Guid> Test()
        {

            var id = _params.GetId(HttpContext);
            

            //var user = HttpContext.User?.Identity?.Name;
            return id;
        }
    }
}
