using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.DTO;
using TMAS.DB.Models;
using MimeKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Policy;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;

namespace TMAS.BLL.Services
{
    public class EmailService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public EmailService(UserManager<User> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<object> CreateEmailAsync(User createdUser)
        {
            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(createdUser);
            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = $"{_configuration["AppUrl"]}/registration/confirmemail?userid={createdUser.Id}&token={validEmailToken}";
            string content = $"<h1>Welcome to TMAS </h1><p>Please confirm your email by  <a href='{url}'>Clicking here</a></p>";
            await SendEmailAsync(createdUser.Email, "Confirm your email", content );
            return default;
        }

        public async Task SendEmailAsync(string toEmail, string newSubject, string content)
        {
            //var apiKey = _configuration["SendGridAPIKey"];
            //var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("test@auth.com", "JWT Auth Demo");
            //var to = new EmailAddress(toEmail);
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            //var response = await client.SendEmailAsync(msg);

            var fromAddress = new MailAddress("davearmstrong653@gmail.com", "Dave Armstrong");
            var toAddress = new MailAddress(toEmail, "Idris");
            string fromPassword = "009090909";
            string subject = newSubject;
            string body = content;

            var smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml=true
            })
            {
                smtp.Send(message);
            }
        }
    }
}
