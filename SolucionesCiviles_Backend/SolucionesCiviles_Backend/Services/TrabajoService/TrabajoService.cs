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
                IsDeleted = x.IsDeleted,
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
                IsDeleted= false,
               
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

        public void Delete(int id)
        {
            var trabajo = _context.Trabajos.Find(id);

            if (trabajo == null)
                throw new Exception("No se encontró ningun registro");

            trabajo.IsDeleted = true;

            _context.Trabajos.Update(trabajo);
            _context.SaveChanges();
        }

        
        public void Update(TrabajoDto dto)
        {
            if (dto == null)
                throw new Exception("Datos inválidos");

            var trabajo = _context.Trabajos.Find(dto.Id);
            var trab = _context.Trabajos.Include(ti => ti.trabajoImages).ThenInclude(x => x.Image).FirstOrDefault(t => t.Id == dto.Id);

            if (trabajo == null)
                throw new Exception($"No se encontró el trabajo con id {dto.Id}");

            trabajo.Name = dto.Name;
            trabajo.Description = dto.Description;
            trabajo.IsDeleted = dto.IsDeleted;

            if (dto.Images != null)
            {
                var imageList = new List<TrabajoImage>();
                foreach (var image in dto.Images)
                {
                    var fileName = _fileService.SaveImage(image);
                    imageList.Add(new TrabajoImage { Trabajo = trabajo, Image = new Image { FileName = fileName } });
                }

                trabajo.trabajoImages = imageList;
            }

            _context.Trabajos.Update(trabajo);
            _context.SaveChanges();
        }

        public void UpdateWithDeletedImages(TrabajoDto dto)
        {
            if (dto == null)
                throw new Exception("Datos inválidos");

            var trabajo = _context.Trabajos.Include(ti => ti.trabajoImages).ThenInclude(x => x.Image).FirstOrDefault(t => t.Id == dto.Id);

            if (trabajo == null)
                throw new Exception($"No se encontró el trabajo con id {dto.Id}");

            trabajo.Name = dto.Name;
            trabajo.Description = dto.Description;
            trabajo.IsDeleted = dto.IsDeleted;

            if (dto.Images != null)
            {
                var allTrabajoImages = trabajo.trabajoImages;
                var imageList = new List<TrabajoImage>();
                foreach (var image in dto.Images)
                {
                    var fileName = _fileService.SaveImage(image);
                    imageList.Add(new TrabajoImage { Trabajo = trabajo, Image = new Image { FileName = fileName } });
                    allTrabajoImages.Add(new TrabajoImage { Trabajo = trabajo, Image = new Image { FileName = fileName } });
                }


                trabajo.trabajoImages = allTrabajoImages;
            }

            //Se eliminan las imagenes que el usuario eliminó desde la web (se borran los registros de las tablas Image y TrabajoImage)
            if (dto.DeletedImages != null)
            {
                
                foreach (var imageId in dto.DeletedImages)
                {
                    var img = _context.Images.Find(imageId);
                    _context.Images.Remove(img);
                    var trabImg = _context.TrabajosImages.Find(imageId);
                    _context.TrabajosImages.Remove(trabImg);
                }    
            }

            _context.Trabajos.Update(trabajo);
            _context.SaveChanges();
        }

        public TrabajoDto GetById(int id)
        {
            var trabajo = _context.Trabajos.Include(ti => ti.trabajoImages).ThenInclude(x => x.Image).FirstOrDefault(t => t.Id == id);

            var trabajoDto =  new TrabajoDto
            {
                Id = trabajo.Id,
                Name = trabajo.Name,
                Description = trabajo.Description,
                IsDeleted = trabajo.IsDeleted,
                ImagesDto = trabajo.trabajoImages.Select(x => new ImageDto
                {
                    Id = x.Image.Id,
                    FileName = x.Image.FileName,
                    Path = _fileService.GetPathImages(x.Image.FileName)
                }).ToList()
            };

            return trabajoDto;
        }
    }
}
