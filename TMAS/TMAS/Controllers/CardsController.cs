using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        CardService card;
        public CardsController(CardService service)
        {
            card = service;
        }


    }
}
