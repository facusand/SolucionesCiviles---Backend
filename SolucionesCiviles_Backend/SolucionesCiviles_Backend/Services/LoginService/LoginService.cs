using DB.Models;
using SolucionesCiviles_Backend.DTOs;
using SolucionesCiviles_Backend.Handlers;

namespace SolucionesCiviles_Backend.Services.LoginService
{
    public class LoginService: ILoginService
    {
        private SolucionesContext _context;        
        public LoginService(SolucionesContext context)
        {
            _context = context;
        }

        public User CheckUser(UserDTO userLogin)
        {
            var currentUser = _context.Users.FirstOrDefault(user => user.Username.ToLower() == userLogin.Username.ToLower() 
                    && user.Password == userLogin.Password);

            if(currentUser != null)
            {
                return currentUser;
            }
            else
            {
                return null;
            }
        }
    }
}
