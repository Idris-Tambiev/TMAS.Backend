using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;
using TMAS.DB.DTO;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
       private readonly BoardService _board;

        public BoardsController(BoardService service)
        {
            _board = service;
        }

        //[HttpPost("/get/board")]
        //public async Task<IActionResult> GetBoard([FromBody] NewBoardDto model)

        //{
        //    return Ok(await _board.GetOne());
        //}

        [HttpPost("/create/board")]
        public async Task<ActionResult<CreatedBoardDto>> Registrate(NewBoardDto model)
        {

            return Ok(await _board.Create(model));
        }
    }
}
