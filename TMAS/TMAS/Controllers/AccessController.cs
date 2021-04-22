using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.Controllers.Base;
using TMAS.DB.DTO;
using TMAS.DB.Models;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : BaseController
    {
        private readonly BoardsAccessService _boardsAccesService;
        public AccessController(BoardsAccessService boardsAccesService)
        {
            _boardsAccesService = boardsAccesService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<BoardsAccess>> CreateBoardsAccess([FromBody]BoardsAccess access) 
        {
            return Ok(await _boardsAccesService.Create(access));
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<BoardViewDTO>> get()
        {
            var userId = GetUserId();
            return Ok(await _boardsAccesService.Get(userId));
        }

        [HttpGet("get/all/users")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUsers(int id,string text)
        {
            var userId = GetUserId();
            return Ok(await _boardsAccesService.GetAllUsers(id, text, userId));
        }


        [HttpGet("get/assigned/users")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetAssignedUsers(int id,string text)
        {
            var userId = GetUserId();
            return Ok(await _boardsAccesService.GetAssignedUsers(id,text,userId));
        }

        [HttpPost("delete")]
        [Authorize]
        public async Task<ActionResult> GetUsers(BoardsAccess access)
        {
            return Ok(await _boardsAccesService.Delete(access));
        }

    }
}
