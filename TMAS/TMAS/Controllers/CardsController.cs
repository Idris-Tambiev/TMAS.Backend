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
        private readonly CardService _cards;

        public CardsController(CardService service)
        {
            _cards = service;
        }

        [HttpGet("/get/cards")]
        [Authorize]
        public async Task<ActionResult<Card>> GetCards(int id)
        {
            return Ok(_cards.GetAll(id));
        }

        [HttpPost("/create/card")]
        [Authorize]
        public async Task<ActionResult<Card>> CreateNewCard(Card card)
        {
            return Ok(await _cards.Create(card));
        }

        [HttpPost("/update/card")]
        [Authorize]
        public async Task<ActionResult<Card>> UpdateCard(Card card)
        {
            return Ok(await _cards.Update(card));
        }

        [HttpPost("/delete/card")]
        [Authorize]
        public async Task<ActionResult<Card>> DeleteCard(int id)
        {
            return Ok(_cards.Delete(id));
        }
    }
}
