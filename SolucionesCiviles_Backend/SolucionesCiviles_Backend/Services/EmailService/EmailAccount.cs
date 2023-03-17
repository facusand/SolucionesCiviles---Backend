namespace SolucionesCiviles_Backend.Services.EmailService
{
    public class EmailAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }

        public EmailAccount(string username, string password, string host)
        {
            Username = username;
            Password = password;
            Host = host;
        }
    }
}
