namespace SolucionesCiviles_Backend.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);

        void SendOpinion(EmailDto request);
    }
}
