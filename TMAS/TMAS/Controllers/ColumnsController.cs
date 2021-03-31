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
        private readonly ColumnService _columnService;

        public ColumnsController(ColumnService service)
        {
            _columnService = service;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<Column>> GetColumns(int id)
        {
            return Ok(_columnService.GetAll(id));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<Column>> CreateNewColumn(Column column)
        {
            return Ok(await _columnService.Create(column));
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<Column>> UpdateColumn(Column column)
        {
            return Ok(await _columnService.Update(column));
        }

        [HttpPost("delete")]
        [Authorize]
        public async Task<ActionResult<Column>> DeleteColumn(int id)
        {
            return Ok(_columnService.Delete(id));
        }
    }
}
