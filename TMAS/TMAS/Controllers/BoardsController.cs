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

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
       private readonly BoardService _boardService;
       private readonly Base.UserParams _params;
        public BoardsController(BoardService service,Base.UserParams userParams)
        {
            _boardService = service;
            _params = userParams;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<IActionResult> GetBoards()
        {
            var id = _params.GetId(HttpContext);
            return Ok(await _boardService.GetAll(id));
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> FindBoards(string text)
        {
            var id = _params.GetId(HttpContext);
            return Ok(await _boardService.FindBoard(id,text));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<Board>> CreateNewBoard(Board board)
        {
            var id = _params.GetId(HttpContext);
            return Ok(await _boardService.Create(board, id));
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<Board>> UpdateBoard(Board board)
        {
            //var idUser = _params.GetId(HttpContext);
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
