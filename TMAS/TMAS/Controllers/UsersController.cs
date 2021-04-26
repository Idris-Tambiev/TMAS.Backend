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
using System.IO;
using System.Net.Http.Headers;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly UserService _userService;
        public UsersController(UserService service )
        {
            _userService = service;
        }

        [HttpPost("create")]
        public async Task<ActionResult<User>> Registrate(RegistrateUserDto model)
        {
            return Ok(await _userService.Create(model));
        }

        [HttpGet("find")]
        public async Task<ActionResult<Response>> Search(string email)
        {
            return Ok(await _userService.Find(email));
        }


        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUserName()
        {
            var id = GetUserId();
            return await _userService.GetOneById(id);
        }

        [HttpGet("getfull")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetFullUSer(string id)
        {
            Guid idUser = Guid.Parse(id);
            return await _userService.GetOneById(idUser);
        }

        [HttpPost("upload/photo")]
        [Authorize]
        public async Task<ActionResult> Upload()
        {
            var id = GetUserId();
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    Guid guidName = Guid.NewGuid();
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var splitedName = fileName.Split('.');
                    var finalName = guidName.ToString() + '.' + splitedName[splitedName.Length - 1];
                    var fullPath = Path.Combine(pathToSave, finalName);
                    var dbPath = Path.Combine(folderName, finalName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var saveResult = await _userService.AddPhoto(id,finalName);
                    return Ok(saveResult);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
