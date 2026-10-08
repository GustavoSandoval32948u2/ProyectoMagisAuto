using MagisAuto.Application.DTOs.Departamento;
using MagisAuto.Application.Interfaces.Departamento;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(
            IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartamentoDto>>> ObtenerTodos()
        {
            var departamentos =
                await _departamentoService.ObtenerTodosAsync();

            return Ok(departamentos);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Buscar(
            [FromQuery] string? criterio)
        {
            var departamentos =
                await _departamentoService.BuscarAsync(criterio);

            return Ok(departamentos);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Crear(
            CrearDepartamentoDto departamento)
        {
            var departamentoId =
                await _departamentoService.CrearAsync(departamento);

            return Ok(departamentoId);
        }

        [HttpPut("{departamentoId:int}")]
        public async Task<IActionResult> Actualizar(
            int departamentoId,
            ActualizarDepartamentoDto departamento)
        {
            var actualizado =
                await _departamentoService.ActualizarAsync(
                    departamentoId,
                    departamento);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{departamentoId:int}")]
        public async Task<IActionResult> Eliminar(int departamentoId)
        {
            var eliminado =
                await _departamentoService.EliminarAsync(departamentoId);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
