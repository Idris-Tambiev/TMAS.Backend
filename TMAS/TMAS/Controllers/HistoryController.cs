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
using TMAS.DAL.DTO;
using TMAS.BLL.Interfaces;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : BaseController
    {
        private readonly IHistoryService _historyService;
        public HistoryController (IHistoryService service)
        {
            _historyService = service;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<HistoryViewDTO>> GetHistory(int id)
        {
            var histories = await _historyService.GetAll(id);
            return Ok(histories);
        }

        //[HttpPost("create")]
        //[Authorize]
        //public async Task<ActionResult<History>> CreateNewHistory(History history)
        //{
        //    var id = GetUserId();
        //    return Ok(await _historyService.Create(history,id));
        //}
    }
}
