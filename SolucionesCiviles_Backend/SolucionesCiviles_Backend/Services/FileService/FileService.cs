using Microsoft.Net.Http.Headers;

namespace SolucionesCiviles_Backend.Services.FileService
{
    public class FileService: IFileService
    {
        public FileService()
        {

        }

        public string SaveImage(IFormFile file)
        {
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Value.Trim('"');
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        public string GetPathImages(string fileName)
        {
            return $"Resources/Images/{fileName}";
        }
    }
}
