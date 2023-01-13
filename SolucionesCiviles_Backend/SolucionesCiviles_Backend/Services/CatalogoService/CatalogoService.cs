using DB.Models;
using Microsoft.EntityFrameworkCore;
using SolucionesCiviles_Backend.DTOs;

namespace SolucionesCiviles_Backend.Services.CatalogoService
{
    public class CatalogoService: ICatalogoService
    {
        private SolucionesContext _context;

        public CatalogoService(SolucionesContext context)
        {
            _context = context;
        }

        public async Task<List<CatalogoDto>> GetAll()
        {
            var catalogos = await _context.Catalogos.ToListAsync();

            if (catalogos == null)
                throw new Exception("No se encontraron registros");


            var catalogoDto = catalogos.Select(x => new CatalogoDto
            {
                Id = x.Id,
                Name = x.Name,
                Link = x.Link,
                IsDeleted = x.IsDeleted,
            }).ToList();

            return catalogoDto;
        }

        public void Add(CatalogoDto dto)
        {
            var cat = new Catalogo
            {
                Name = dto.Name,
                Link = dto.Link,
                IsDeleted = false
            };
            _context.Catalogos.Add(cat);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var catalogo = _context.Catalogos.Find(id);

            if (catalogo == null)
                throw new Exception("No se encontró ningun registro");

            catalogo.IsDeleted = true;

            _context.Catalogos.Update(catalogo);
            _context.SaveChanges();
        }

        public void Update(CatalogoDto dto)
        {
            if (dto == null)
                throw new Exception("Datos inválidos");

            var catalogo = _context.Catalogos.Find(dto.Id);

            if (catalogo == null)
                throw new Exception($"No se encontró el catalogo con id {dto.Id}");

            catalogo.Name = dto.Name;
            catalogo.Link = dto.Link;

            _context.Catalogos.Update(catalogo);
            _context.SaveChanges();
        }
    }
}
