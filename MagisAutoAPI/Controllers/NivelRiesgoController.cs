using MagisAuto.Application.DTOs.NivelRiesgo;
using MagisAuto.Application.Interfaces.NivelRiesgo;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NivelRiesgoController : ControllerBase
{
    private readonly INivelRiesgoService _nivelRiesgoService;

    public NivelRiesgoController(INivelRiesgoService nivelRiesgoService)
    {
        _nivelRiesgoService = nivelRiesgoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NivelRiesgoDto>>> ObtenerTodos()
    {
        return Ok(await _nivelRiesgoService.ObtenerTodosAsync());
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<NivelRiesgoDto>>> Buscar(
        [FromQuery] string? criterio)
    {
        return Ok(await _nivelRiesgoService.BuscarAsync(criterio));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(
        CrearNivelRiesgoDto dto)
    {
        var id = await _nivelRiesgoService.CrearAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarNivelRiesgoDto dto)
    {
        await _nivelRiesgoService.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _nivelRiesgoService.EliminarAsync(id);

        return NoContent();
    }
}