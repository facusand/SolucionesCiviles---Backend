using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.AspNetCore.Mvc;

namespace SolucionesCiviles_Backend.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        List<EmailAccount> emailAccounts;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void LoadAccounts()
        {
            emailAccounts = new List<EmailAccount>()
            {
                new EmailAccount(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value,_config.GetSection("EmailHost").Value ),
                new EmailAccount(_config.GetSection("EmailUsername2").Value, _config.GetSection("EmailPassword2").Value,_config.GetSection("EmailHost").Value ),
                new EmailAccount(_config.GetSection("EmailUsername3").Value, _config.GetSection("EmailPassword3").Value,_config.GetSection("EmailHost").Value ),
                new EmailAccount(_config.GetSection("EmailUsername4").Value, _config.GetSection("EmailPassword4").Value,_config.GetSection("EmailHost").Value )
            };
        }

        public void SendEmail(EmailDto request)
        {
            LoadAccounts();

            foreach (var account in emailAccounts)
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(request.UserEmail));
                email.To.Add(MailboxAddress.Parse(account.Username));
                email.Subject = "Mensaje de un visitante";

                var builder = new BodyBuilder();
                email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                using var smtp = new SmtpClient();
                smtp.Connect(account.Host, 465, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(account.Username, account.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            /*
            var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.From.Add(MailboxAddress.Parse(request.UserEmail));
            email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername2").Value));
            email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername3").Value));
            //email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername4").Value));
            email.Subject = "Mensaje de un visitante";

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Connect(_config.GetSection("EmailHost").Value, 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);*/
        }

        public void SendOpinion(EmailDto request)
        {
            LoadAccounts();


                foreach (var account in emailAccounts)
                {
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse(request.UserEmail));
                    email.To.Add(MailboxAddress.Parse(account.Username));
                    email.Subject = "Opinión sobre revista";

                    var builder = new BodyBuilder();
                    email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                    using var smtp = new SmtpClient();
                    smtp.Connect(account.Host, 465, SecureSocketOptions.SslOnConnect);
                    smtp.Authenticate(account.Username, account.Password);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }


        }
    }
}
