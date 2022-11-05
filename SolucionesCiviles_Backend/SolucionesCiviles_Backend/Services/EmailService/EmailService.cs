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
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.Subject = "Message from a visitor";

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            builder.HtmlBody = "<P>Nombre de usuario: "+request.Name 
                                + "<br> <br>Email del usuario: " + request.UserEmail 
                                +"<br> <br> Mensaje: " + request.Body;
            email.Body = builder.ToMessageBody();

            //email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
