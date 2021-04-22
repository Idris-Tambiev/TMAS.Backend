using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.Controllers.Base;
using TMAS.DB.Models;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : BaseController
    {
        private readonly UserService _userService;
        private readonly EmailService _emailService;
        private readonly UserManager<User> _userManager;

        public EmailsController(UserService userService, EmailService emailService, UserManager<User> userManager)
        {
            _userService = userService;
            _emailService = emailService;
            _userManager = userManager;
        }
        // api/emails/confirmemail
        [HttpGet("confirm/email")]
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpGet("reset")]
        public async Task<ActionResult> ResetPassword(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            var result = _emailService.CreateResetEmail(user);
            return Ok(result);
        }

        [HttpGet("confirm/new/password")]
        public async Task<ActionResult> ConfirmResetPassword(string userId, string token, string password)
        {
            var result = await _userService.ResetUserPassword(userId, token, password);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
