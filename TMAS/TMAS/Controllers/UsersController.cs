using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;

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
        public async Task<IActionResult> Login ([FromBody]User model)
        {
            return Ok(await _user.GetOneByEmail(model));
        }

        [HttpPost("/create/user")]
        public async Task<IActionResult> Registrate( User model)
        {
            return Ok(await _user.Create(model));
        }
    }
}
