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
    public class HistoryController : ControllerBase
    {
        HistoryService _historyService;
        private readonly Base.UserParams _params;
        public HistoryController (HistoryService service,Base.UserParams userParams)
        {
            _historyService = service;
            _params = userParams;
        }


        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<History>> GetHistory()
        {
            var id = _params.GetId(HttpContext);
            return Ok(_historyService.GetAll(id));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<History>> CreateNewCard(int actionId,string text)
        {
            var id = _params.GetId(HttpContext);
            return Ok(await _historyService.Create(actionId, text,id));
        }
    }
}
