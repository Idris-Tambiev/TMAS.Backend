using Microsoft.AspNetCore.Authorization;
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
    public class ColumnsController : ControllerBase
    {
        private readonly ColumnService _column;

        public ColumnsController(ColumnService service)
        {
            _column = service;
        }

        [HttpGet("/get/columns")]
        [Authorize]
        public async Task<ActionResult<Column>> GetColumns(int id)
        {
            return Ok( _column.GetAll(id));
        }

        [HttpPost("/create/column")]
        [Authorize]
        public async Task<ActionResult<Column>> CreateNewColumn(Column column)
        {
            return Ok(await _column.Create(column));
        }

        [HttpPost("/update/column")]
        [Authorize]
        public async Task<ActionResult<Column>> UpdateBoard(Column column)
        {
            return Ok(await _column.Update(column));
        }

        [HttpPost("/delete/column")]
        [Authorize]
        public async Task<ActionResult<Column>> DeleteBoard(int id)
        {
            return Ok(_column.Delete(id));
        }
    }
}
