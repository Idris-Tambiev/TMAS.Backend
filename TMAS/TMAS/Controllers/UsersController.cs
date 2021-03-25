using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserService _user;
        public UsersController(UserService service)
        {
            _user = service;
        }

        [HttpPost("/new/user")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            return default;
        }
    }
}
