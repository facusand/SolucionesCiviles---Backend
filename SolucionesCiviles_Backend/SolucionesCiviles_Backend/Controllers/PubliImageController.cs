using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolucionesCiviles_Backend.DTOs;
using SolucionesCiviles_Backend.Services.PubliImageService;
using System.Net;

namespace SolucionesCiviles_Backend.Controllers
{
    [Route("api/publiImage")]
    [ApiController]
    public class PubliImageController : ControllerBase
    {
        private readonly IPubliImageService _PubliImageService;

        public PubliImageController(IPubliImageService publiImage, SolucionesContext context)
        {
            _PubliImageService = publiImage;
        }

        [HttpPost("create")]
        [Authorize(Roles = ("admin"))]
        public IActionResult AddPublicidad([FromForm] PubliImageDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Datos invalidos");

                var files = Request.Form.Files;
                if (files.Count == 0) // verificar si hay archivos en la solicitud
                    return BadRequest("No se ha adjuntado ningún archivo");

                dto.Image = files;

                _PubliImageService.Add(dto);
                return Ok(new { HttpStatusCode.Created, message = "Creado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error al crear. {ex.Message}");
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var publiImages= await _PubliImageService.GetAll();
            return Ok(publiImages);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = ("admin"))]
        public IActionResult Delete(int id)
        {
            try
            {
                _PubliImageService.Delete(id);

                return Ok(new { HttpStatusCode.Accepted, message = "Eliminado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrión un error al eliminar. {ex.Message}");
            }
        }

        [HttpPost("update")]
        [Authorize(Roles = ("admin"))]
        public IActionResult Update([FromForm] PubliImageDto dto)
        {
            try
            {

                _PubliImageService.Update(dto);

                return Ok(new { HttpStatusCode.Accepted, message = "Actualizado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error en la actualización. {ex.Message}");
            }
        }
    }
}
