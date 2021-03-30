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

        [HttpGet("/get/boards")]
        [Authorize]
        public IActionResult GetBoards()
        {
            var id = _params.GetId(HttpContext);
            return Ok(_boardService.GetAll(id));
        }

        [HttpGet("/search/boards")]
        [Authorize]
        public IActionResult FindBoards(string text)
        {
            var id = _params.GetId(HttpContext);
            return Ok(_boardService.FindBoard(id,text));
        }

        [HttpPost("/create/board")]
        [Authorize]
        public async Task<ActionResult<Board>> CreateNewBoard(string title)
        {
            var id = _params.GetId(HttpContext);
            return Ok(await _boardService.Create(title,id));
        }

        [HttpPost("/update/board")]
        [Authorize]
        public async Task<ActionResult<Board>> UpdateBoard(Board board)
        {
            //var idUser = _params.GetId(HttpContext);
            return Ok(_boardService.Update(board));
        }

        [HttpPost("/delete/board")]
        [Authorize]
        public async Task<ActionResult<Board>> DeleteBoard(int id)
        {
            return Ok(_boardService.Delete(id));
        }
    }
}
