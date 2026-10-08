using MagisAuto.Application.DTOs.Marca;
using MagisAuto.Application.Interfaces.Marca;
using MagisAuto.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;

        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaDto>>> ObtenerTodos()
        {
            var marcas = await _marcaService.ObtenerTodosAsync();

            return Ok(marcas);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<MarcaDto>>> Buscar(
            [FromQuery] string? criterio)
        {
            var marcas = await _marcaService.BuscarAsync(criterio);

            return Ok(marcas);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Crear(CrearMarcaDto marca)
        {
            var marcaId = await _marcaService.CrearAsync(marca);

            return Ok(marcaId);
        }

        [HttpPut("{marcaId:int}")]
        public async Task<IActionResult> Actualizar(
            int marcaId,
            ActualizarMarcaDto marca)
        {
            var actualizado = await _marcaService.ActualizarAsync(
                marcaId,
                marca);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{marcaId:int}")]
        public async Task<IActionResult> Eliminar(int marcaId)
        {
            var eliminado = await _marcaService.EliminarAsync(marcaId);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
