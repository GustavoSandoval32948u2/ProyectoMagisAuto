using MagisAuto.Application.DTOs.Agencia;
using MagisAuto.Application.Interfaces.Agencia;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgenciaController : ControllerBase
    {
        private readonly IAgenciaService _agenciaService;

        public AgenciaController(IAgenciaService agenciaService)
        {
            _agenciaService = agenciaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgenciaDto>>> ObtenerTodos()
        {
            var agencias = await _agenciaService.ObtenerTodosAsync();

            return Ok(agencias);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<AgenciaDto>>> Buscar(
            [FromQuery] string? criterio)
        {
            var agencias = await _agenciaService.BuscarAsync(criterio);

            return Ok(agencias);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Crear(CrearAgenciaDto agencia)
        {
            var agenciaId = await _agenciaService.CrearAsync(agencia);

            return Ok(agenciaId);
        }

        [HttpPut("{agenciaId:int}")]
        public async Task<IActionResult> Actualizar(
            int agenciaId,
            ActualizarAgenciaDto agencia)
        {
            var actualizado = await _agenciaService.ActualizarAsync(
                agenciaId,
                agencia);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{agenciaId:int}")]
        public async Task<IActionResult> Eliminar(int agenciaId)
        {
            var eliminado = await _agenciaService.EliminarAsync(agenciaId);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
