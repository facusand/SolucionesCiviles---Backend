using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DB.Models;
using SolucionesCiviles_Backend.Services.TrabajoService;
using Microsoft.EntityFrameworkCore;
using SolucionesCiviles_Backend.DTOs;
using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace SolucionesCiviles_Backend.Controllers
{
    [Route("api/trabajo")]
    [ApiController]
    public class TrabajoController : ControllerBase
    {
        private readonly ITrabajoService _trabajoService;
        private readonly SolucionesContext _context;

        public TrabajoController(ITrabajoService trabajoService, SolucionesContext context)
        {
            _trabajoService = trabajoService;
            _context = context;
        }


        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var trabajos = await _trabajoService.GetAll();
            return Ok(trabajos);
        }

        [HttpPost("create")]
        [Authorize(Roles = ("admin"))]
        public IActionResult AddTrabajo([FromForm] TrabajoDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Datos invalidos");

                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    dto.Images = files;
                }

                _trabajoService.Add(dto);
                //await _context.Trabajos.AddAsync(trabajo);
                //await _context.SaveChangesAsync();
                return Ok(new { HttpStatusCode.Created, message = "Creado con éxito" });
            }
            catch(Exception ex)
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
                _trabajoService.Delete(id);
                
                return Ok(new { HttpStatusCode.Accepted, message = "Eliminado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrión un error al eliminar. {ex.Message}");
            }
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] TrabajoDto dto)
        {
            try
            {
                var files = Request.Form.Files;
                if (dto == null)
                    return BadRequest("Datos invalidos");

                if (files.Count > 0)
                {
                    dto.Images = files;
                }

                _trabajoService.Update(dto);

                return Ok(new { HttpStatusCode.Accepted, message = "Actualizado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error en la actualización. {ex.Message}");
            }
        }
    }
}
