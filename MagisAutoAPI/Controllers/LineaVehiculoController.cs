using MagisAuto.Application.DTOs.LineaVehiculo;
using MagisAuto.Application.Interfaces.LineaVehiculo;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LineaVehiculoController : ControllerBase
    {
        private readonly ILineaVehiculoService _lineaVehiculoService;

        public LineaVehiculoController(ILineaVehiculoService lineaVehiculoService)
        {
            _lineaVehiculoService = lineaVehiculoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineaVehiculoDto>>> ObtenerTodos()
        {
            var lineaVehiculos = await _lineaVehiculoService.ObtenerTodosAsync();

            return Ok(lineaVehiculos);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<LineaVehiculoDto>>> Buscar(
            [FromQuery] string? criterio)
        {
            var lineaVehiculos = await _lineaVehiculoService.BuscarAsync(criterio);

            return Ok(lineaVehiculos);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Crear(CrearLineaVehiculoDto lineaVehiculo)
        {
            var lineaVehiculoId = await _lineaVehiculoService.CrearAsync(lineaVehiculo);

            return Ok(lineaVehiculoId);
        }

        [HttpPut("{lineaVehiculoId:int}")]
        public async Task<IActionResult> Actualizar(
            int lineaVehiculoId,
            ActualizarLineaVehiculoDto lineaVehiculo)
        {
            var actualizado = await _lineaVehiculoService.ActualizarAsync(
                lineaVehiculoId,
                lineaVehiculo);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{lineaVehiculoId:int}")]
        public async Task<IActionResult> Eliminar(int lineaVehiculoId)
        {
            var eliminado = await _lineaVehiculoService.EliminarAsync(lineaVehiculoId);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
