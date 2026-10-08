using MagisAuto.Application.DTOs.TipoTelefono;
using MagisAuto.Application.Interfaces.TipoTelefono;
using MagisAuto.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoTelefonoController : ControllerBase
    {
        private readonly ITipoTelefonoService _tipoTelefonoService;

        public TipoTelefonoController(ITipoTelefonoService tipoTelefonoService)
        {
            _tipoTelefonoService = tipoTelefonoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTelefonoDto>>> ObtenerTodos()
        {
            var tipoTelefonos = await _tipoTelefonoService.ObtenerTodosAsync();

            return Ok(tipoTelefonos);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<TipoTelefonoDto>>> Buscar(
            [FromQuery] string? criterio)
        {
            var tipoTelefonos = await _tipoTelefonoService.BuscarAsync(criterio);

            return Ok(tipoTelefonos);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Crear(
            CrearTipoTelefonoDto tipoTelefono)
        {
            var tipoTelefonoId =
                await _tipoTelefonoService.CrearAsync(tipoTelefono);

            return Ok(tipoTelefonoId);
        }

        [HttpPut("{tipoTelefonoId:int}")]
        public async Task<IActionResult> Actualizar(
            int tipoTelefonoId,
            ActualizarTipoTelefonoDto tipoTelefono)
        {
            var actualizado =
                await _tipoTelefonoService.ActualizarAsync(
                    tipoTelefonoId,
                    tipoTelefono);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{tipoTelefonoId:int}")]
        public async Task<IActionResult> Eliminar(int tipoTelefonoId)
        {
            var eliminado =
                await _tipoTelefonoService.EliminarAsync(tipoTelefonoId);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
