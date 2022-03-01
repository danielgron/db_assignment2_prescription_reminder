using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Assignment2.Interfaces;
using Pharmacy_Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy_Assignment2.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailService _mailService;
        public EmailController(IMailService mailService)
        {
            _mailService = mailService;

        }


        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest mailRequest)
        {
            try
            {
                await _mailService.SendEmailAsync(mailRequest);
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
