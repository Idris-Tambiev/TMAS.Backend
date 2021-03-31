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
    public class CardsController : ControllerBase
    {
        private readonly CardService _cardsService;
        private readonly Base.UserParams _params;
        public CardsController(CardService service,Base.UserParams userParams)
        {
            _cardsService = service;
            _params = userParams;
        }

        [HttpGet("search")]
        [Authorize]
        public IActionResult FindBoards(string text)
        {
            var id = _params.GetId(HttpContext);
            return Ok(_cardsService.FindCard(id, text));

        }
        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<Card>> GetCards(int id)
        {
            return Ok(_cardsService.GetAll(id));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<Card>> CreateNewCard(Card card)
        {
            return Ok(await _cardsService.Create(card));
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<Card>> UpdateCard(Card card)
        {
            return Ok(await _cardsService.Update(card));
        }

        [HttpPost("delete")]
        [Authorize]
        public async Task<ActionResult<Card>> DeleteCard(int id)
        {
            return Ok(_cardsService.Delete(id));
        }
    }
}
