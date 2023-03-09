using SolucionesCiviles_Backend.DTOs;

namespace SolucionesCiviles_Backend.Services.PubliImageService
{
    public interface IPubliImageService
    {
        Task<List<PubliImageDto>> GetAll();
        void Add(PubliImageDto publiImage);
        void Delete(int id);
        void Update(PubliImageDto dto);
    }
}
