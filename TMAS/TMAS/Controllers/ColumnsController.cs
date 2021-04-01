using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;
using TMAS.Controllers.Base;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly ColumnService _columnService;

        public ColumnsController(ColumnService service)
        {
            _columnService = service;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<Column>> GetColumns(int id)
        {
            return Ok(await _columnService.GetAll(id));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<Column>> CreateNewColumn(string title,int boardId)
        {
            return Ok(await _columnService.Create(title,boardId));
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<Column>> UpdateColumn(Column column)
        {
            return Ok(await _columnService.Update(column));
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<Column>> DeleteColumn(int id)
        {
            return Ok(await _columnService.Delete(id));
        }
    }
}
