using MagisAuto.Application.DTOs.Vehiculo;
using MagisAuto.Application.Interfaces.Vehiculo;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;

        public VehiculoController(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiculoDto>>> ObtenerTodos()
        {
            var vehiculos = await _vehiculoService.ObtenerTodosAsync();

            return Ok(vehiculos);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<VehiculoDto>>> Buscar(
            [FromQuery] string? criterio)
        {
            var vehiculos = await _vehiculoService.BuscarAsync(criterio);

            return Ok(vehiculos);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Crear(CrearVehiculoDto vehiculo)
        {
            var vehiculoId = await _vehiculoService.CrearAsync(vehiculo);

            return Ok(vehiculoId);
        }

        [HttpPut("{vehiculoId:int}")]
        public async Task<IActionResult> Actualizar(
            int vehiculoId,
            ActualizarVehiculoDto vehiculo)
        {
            var actualizado = await _vehiculoService.ActualizarAsync(
                vehiculoId,
                vehiculo);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{vehiculoId:int}")]
        public async Task<IActionResult> Eliminar(int vehiculoId)
        {
            var eliminado = await _vehiculoService.EliminarAsync(vehiculoId);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
