using Microsoft.AspNetCore.Authorization;
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
    public class ColumnsController : ControllerBase
    {
        ColumnService column;

        public ColumnsController(ColumnService service)
        {
            column = service;
        }
    }
}
