﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;
using TMAS.Controllers.Base;
using TMAS.DB.DTO;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : BaseController
    {
        HistoryService _historyService;
        public HistoryController (HistoryService service)
        {
            _historyService = service;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<HistoryViewDTO>> GetHistory(int id)
        {
            //var id = GetUserId();
            return Ok( await _historyService.GetAll(id));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<History>> CreateNewHistory(History history)
        {
            var id = GetUserId();
            return Ok(await _historyService.Create(history,id));
        }
    }
}
