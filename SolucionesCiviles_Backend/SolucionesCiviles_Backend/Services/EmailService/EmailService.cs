using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace SolucionesCiviles_Backend.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.From.Add(MailboxAddress.Parse(request.UserEmail));
            email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.Subject = "Mensaje de un visitante";

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            //smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Connect(_config.GetSection("EmailHost").Value, 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendOpinion(EmailDto request)
        {
            var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.From.Add(MailboxAddress.Parse(request.UserEmail));
            email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.Subject = "Opinión sobre revista";

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            //smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Connect(_config.GetSection("EmailHost").Value, 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
