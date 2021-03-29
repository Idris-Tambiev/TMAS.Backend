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
       private readonly BoardService _board;
       private readonly Base.UserParams _params;
        public BoardsController(BoardService service,Base.UserParams userParams)
        {
            _board = service;
            _params = userParams;
        }

        [HttpGet("/get/boards")]
        [Authorize]
        public async Task<IActionResult> GetBoard()
        {
            var id = _params.GetId(HttpContext);
            return Ok(_board.GetAll(id));
        }

        [HttpPost("/create/board")]
        [Authorize]
        public async Task<ActionResult<Board>> CreateNewBoard(string title)
        {
            var id = _params.GetId(HttpContext);
            return Ok(await _board.Create(title,id));
        }
    }
}
