using Microsoft.Net.Http.Headers;

namespace SolucionesCiviles_Backend.Services.FileService
{
    public interface IFileService
    {
        string SaveImage(IFormFile file);

        string GetPathImages(string fileName);
    }
}
