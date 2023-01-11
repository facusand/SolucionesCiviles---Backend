using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolucionesCiviles_Backend.DTOs;
using SolucionesCiviles_Backend.Services.CatalogoService;
using System.Net;

namespace SolucionesCiviles_Backend.Controllers
{
    [Route("api/catalogo")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;
        private readonly SolucionesContext _context;

        public CatalogoController(ICatalogoService catalogoService, SolucionesContext context)
        {
            _catalogoService = catalogoService;
            _context = context;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var catalogos = await _catalogoService.GetAll();
            return Ok(catalogos);
        }

        [HttpPost("create")]
        //[Authorize(Roles = ("admin"))]
        public IActionResult AddCatalogo([FromForm] CatalogoDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Datos invalidos");

                _catalogoService.Add(dto);
                //await _context.Trabajos.AddAsync(trabajo);
                //await _context.SaveChangesAsync();
                return Ok(new { HttpStatusCode.Created, message = "Creado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error al crear. {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = ("admin"))]
        public IActionResult Delete(int id)
        {
            try
            {
                _catalogoService.Delete(id);

                return Ok(new { HttpStatusCode.Accepted, message = "Eliminado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrión un error al eliminar. {ex.Message}");
            }
        }

        [HttpPost("update")]
        [Authorize(Roles = ("admin"))]
        public IActionResult Update([FromForm] CatalogoDto dto)
        {
            try
            {

                _catalogoService.Update(dto);

                return Ok(new { HttpStatusCode.Accepted, message = "Actualizado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error en la actualización. {ex.Message}");
            }
        }
    }
}
