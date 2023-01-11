using DB.Models;
using SolucionesCiviles_Backend.DTOs;

namespace SolucionesCiviles_Backend.Services.LoginService
{
    public interface ILoginService
    {
        User CheckUser(UserDTO user);
    }
}
