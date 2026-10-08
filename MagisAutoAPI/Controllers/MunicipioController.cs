using MagisAuto.Application.DTOs.Municipio;
using MagisAuto.Application.Interfaces.Municipio;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MunicipioController : ControllerBase
    {
        private readonly IMunicipioService _municipioService;

        public MunicipioController(IMunicipioService municipioService)
        {
            _municipioService = municipioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MunicipioDto>>> ObtenerTodos()
        {
            var municipios =
                await _municipioService.ObtenerTodosAsync();

            return Ok(municipios);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<MunicipioDto>>> Buscar(
            [FromQuery] string? criterio)
        {
            var municipios =
                await _municipioService.BuscarAsync(criterio);

            return Ok(municipios);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Crear(
            CrearMunicipioDto municipio)
        {
            var municipioId =
                await _municipioService.CrearAsync(municipio);

            return Ok(municipioId);
        }

        [HttpPut("{municipioId:int}")]
        public async Task<IActionResult> Actualizar(
            int municipioId,
            ActualizarMunicipioDto municipio)
        {
            var actualizado =
                await _municipioService.ActualizarAsync(
                    municipioId,
                    municipio);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{municipioId:int}")]
        public async Task<IActionResult> Eliminar(int municipioId)
        {
            var eliminado =
                await _municipioService.EliminarAsync(municipioId);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
