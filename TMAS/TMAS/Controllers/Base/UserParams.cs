using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMAS.Controllers.Base
{
    public class UserParams:ControllerBase
    {

        public Guid GetId(HttpContext httpContext)
        {
            var claims = httpContext.User.Claims.ToList();
            string[] words = claims[4].ToString().Split(' ');
            Guid id = Guid.Parse(words[1]);
            return id;
        }
    }
}
