using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace SolucionesCiviles_Backend.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        [HttpPost]
        public IActionResult SendEmail(EmailDto request)
        {
            try
            {
                _emailService.SendEmail(request);
                return Ok(new { message = "Mensaje enviado!" });
            }
            catch (Exception ex)
            {
                return BadRequest("Error al enviar el mensaje: " + ex.Message);
            }
            
        }

        [HttpPost("opinion")]
        public IActionResult SendOpinion(EmailDto request)
        {
            try
            {
                _emailService.SendOpinion(request);
                return Ok(new { message = "Mensaje enviado!" });
            }
            catch (Exception ex)
            {
                return BadRequest("Error al enviar el mensaje: " + ex.Message);
            }
            
        }
    }
}
