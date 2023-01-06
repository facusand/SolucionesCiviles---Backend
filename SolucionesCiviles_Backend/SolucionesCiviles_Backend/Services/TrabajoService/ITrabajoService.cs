using DB.Models;
using Microsoft.AspNetCore.Mvc;
using SolucionesCiviles_Backend.DTOs;

namespace SolucionesCiviles_Backend.Services.TrabajoService
{
    public interface ITrabajoService
    {
        Task<List<TrabajoDto>> GetAll();
        void Add(TrabajoDto trabajo);
    }
}
