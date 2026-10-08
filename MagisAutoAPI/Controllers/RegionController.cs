using MagisAuto.Application.DTOs.Region;
using MagisAuto.Application.Interfaces.Region;
using MagisAuto.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
           _regionService = regionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionDto>>> ObtenerTodos()
        {
            var regiones = await _regionService.ObtenerTodosAsync();

            return Ok(regiones);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<RegionDto>>> Buscar(
            [FromQuery] string? criterio)
        {
            var region = await _regionService.BuscarAsync(criterio);

            return Ok(region);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Crear(CrearRegionDto region)
        {
            var regionId = await _regionService.CrearAsync(region);

            return Ok(regionId);
        }

        [HttpPut("{regionId:int}")]
        public async Task<IActionResult> Actualizar(
            int regionId,
            ActualizarRegionDto region)
        {
            var actualizado = await _regionService.ActualizarAsync(
                regionId,
                region);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{regionId:int}")]
        public async Task<IActionResult> Eliminar(int regionId)
        {
            var eliminado = await _regionService.EliminarAsync(regionId);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
