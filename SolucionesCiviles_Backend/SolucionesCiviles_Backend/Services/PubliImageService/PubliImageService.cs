using DB.Models;
using SolucionesCiviles_Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SolucionesCiviles_Backend.Services.PubliImageService
{
    public class PubliImageService : IPubliImageService
    {   
        private IFileService _fileService;
        private SolucionesContext _context;

        public PubliImageService(IFileService fileService, SolucionesContext context)
        {
            _fileService = fileService;
            _context = context;
        }

        public void Add(PubliImageDto dto)
        {
            var publi = new PubliImage
            {
                Descripcion = dto.Descripcion,
                IsDeleted = false,
            };

            if (dto.Image != null)
            {
                var image = dto.Image.FirstOrDefault();
                var fileName = _fileService.SaveImage(image);
                publi.FileName = fileName;
            }
            
            
            _context.PubliImages.Add(publi);
            _context.SaveChanges();
        }

        public async Task<List<PubliImageDto>> GetAll()
        {
            var publiImages = await _context.PubliImages.ToListAsync();

            if (publiImages == null)
                throw new Exception("No se encontraron registros");


            var publiImageDto = publiImages.Select(x => new PubliImageDto
            {
                Id = x.Id,
                Filename = x.FileName,
                Descripcion = x.Descripcion,
                IsDeleted = x.IsDeleted,
                Path = _fileService.GetPathImages(x.FileName),
            }).ToList();

            return publiImageDto;
        }

        public void Delete(int id)
        {
            var publi = _context.PubliImages.Find(id);

            if (publi == null)
                throw new Exception("No se encontró ningun registro");

            publi.IsDeleted = true;

            _context.PubliImages.Update(publi);
            _context.SaveChanges();
        }

        public void Update(PubliImageDto dto)
        {
            if (dto == null)
                throw new Exception("Datos inválidos");

            var publi = _context.PubliImages.Find(dto.Id);

            if (publi == null)
                throw new Exception($"No se encontró el catalogo con id {dto.Id}");

            publi.Descripcion = dto.Descripcion;
            publi.IsDeleted = dto.IsDeleted;
            if (dto.Image != null)
            {
                var image = dto.Image.FirstOrDefault();
                var fileName = _fileService.SaveImage(image);
                publi.FileName = fileName;
            }
            _context.PubliImages.Update(publi);
            _context.SaveChanges();
        }
    }
}
