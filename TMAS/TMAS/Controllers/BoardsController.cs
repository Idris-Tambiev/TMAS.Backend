using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;
using TMAS.DB.DTO;
using Microsoft.AspNetCore.Authorization;
using TMAS.Controllers.Base;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : BaseController
    {
       private readonly BoardService _boardService;
       //private readonly Base.BaseController _params;
        public BoardsController(BoardService service)
        {
            _boardService = service;
            //_params = userParams;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<IActionResult> GetBoards()
        {
            var id = GetUserId();
            return Ok(await _boardService.GetAll(id));
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> FindBoards(string text)
        {
            var id = GetUserId();
            return Ok(await _boardService.FindBoard(id,text));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<Board>> CreateNewBoard(string title)
        {
            var id = GetUserId();
            return Ok(await _boardService.Create(title, id));
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<Board>> UpdateBoard(Board board)
        {
            //var idUser = GetUserId();
            return Ok(await _boardService.Update(board));
        }

        [HttpPost("delete")]
        [Authorize]
        public async Task<ActionResult<Board>> DeleteBoard(int id)
        {
            return Ok(await _boardService.Delete(id));
        }
    }
}
