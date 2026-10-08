using MagisAuto.Application.DTOs.Parentesco;
using MagisAuto.Application.Interfaces.Parentesco;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentescoController : ControllerBase
    {
        private readonly IParentescoService _parentescoService;

        public ParentescoController(IParentescoService parentescoService)
        {
            _parentescoService = parentescoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentescoDto>>> ObtenerTodos()
        {
            var parentescos = await _parentescoService.ObtenerTodosAsync();

            return Ok(parentescos);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ParentescoDto>>> Buscar(
            [FromQuery] string? criterio)
        {
            var parentescos = await _parentescoService.BuscarAsync(criterio);

            return Ok(parentescos);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Crear(CrearParentescoDto dto)
        {
            var parentescoId = await _parentescoService.CrearAsync(dto);

            return Ok(parentescoId);
        }

        [HttpPut("{parentescoId:int}")]
        public async Task<IActionResult> Actualizar(
            int parentescoId,
            ActualizarParentescoDto dto)
        {
            await _parentescoService.ActualizarAsync(parentescoId, dto);

            return NoContent();
        }

        [HttpDelete("{parentescoId:int}")]
        public async Task<IActionResult> Eliminar(int parentescoId)
        {
            await _parentescoService.EliminarAsync(parentescoId);

            return NoContent();
        }
    }
}
