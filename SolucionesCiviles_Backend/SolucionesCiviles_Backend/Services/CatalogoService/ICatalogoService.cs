using SolucionesCiviles_Backend.DTOs;

namespace SolucionesCiviles_Backend.Services.CatalogoService
{
    public interface ICatalogoService
    {
        Task<List<CatalogoDto>> GetAll();
        void Add(CatalogoDto trabajo);
        void Delete(int id);
        void Update(CatalogoDto dto);
    }
}
