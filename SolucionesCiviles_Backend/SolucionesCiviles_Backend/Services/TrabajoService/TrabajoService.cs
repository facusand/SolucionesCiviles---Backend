using DB.Models;
using SolucionesCiviles_Backend.DTOs;
using SolucionesCiviles_Backend.Services.FileService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SolucionesCiviles_Backend.Services.TrabajoService
{
    public class TrabajoService: ITrabajoService
    {
        private  IFileService _fileService;
        private SolucionesContext _context;

        public TrabajoService(IFileService fileService, SolucionesContext context)
        {
            _fileService = fileService;
            _context = context;
        }
        public async Task<List<TrabajoDto>> GetAll()
        {
            var trabajos = await _context.Trabajos.Include(ti => ti.trabajoImages).ThenInclude(x => x.Image).ToListAsync();
            
            

            if (trabajos == null)
                throw new Exception("No se encontraron registros");

            
            var trabajoDto = trabajos.Select(x => new TrabajoDto
            {
                Id= x.Id,
                Name = x.Name,
                Description = x.Description,
                ImagesDto = x.trabajoImages.Select(x=> new ImageDto { 
                    Id = x.Image.Id,
                    FileName = x.Image.FileName,
                    Path = _fileService.GetPathImages(x.Image.FileName)
                }).ToList()
            }).ToList();
            
            return trabajoDto;
        }

        public void Add(TrabajoDto dto)
        {
            var trab = new Trabajo
            {
                Name = dto.Name,
                Description = dto.Description,
               
            };
            if (dto.Images != null)
            {
                var imageList = new List<TrabajoImage>();
                foreach (var image in dto.Images)
                {
                    var fileName = _fileService.SaveImage(image);
                    imageList.Add(new TrabajoImage { Trabajo = trab, Image = new Image { FileName = fileName } });
                }

                trab.trabajoImages = imageList;
            }
            _context.Trabajos.Add(trab);
           _context.SaveChanges();
        }
    }
}
