using MagisAuto.Application.DTOs.DireccionPersona;
using MagisAuto.Application.Interfaces.DireccionPersona;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DireccionPersonaController : ControllerBase
{
    private readonly IDireccionPersonaService _direccionPersonaService;

    public DireccionPersonaController(
        IDireccionPersonaService direccionPersonaService)
    {
        _direccionPersonaService = direccionPersonaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DireccionPersonaDto>>>
        ObtenerTodos()
    {
        var direcciones =
            await _direccionPersonaService.ObtenerTodosAsync();

        return Ok(direcciones);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<DireccionPersonaDto>>>
        Buscar([FromQuery] string? criterio)
    {
        var direcciones =
            await _direccionPersonaService.BuscarAsync(criterio);

        return Ok(direcciones);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(
        CrearDireccionPersonaDto dto)
    {
        var direccionPersonaId =
            await _direccionPersonaService.CrearAsync(dto);

        return Ok(direccionPersonaId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarDireccionPersonaDto dto)
    {
        await _direccionPersonaService.ActualizarAsync(
            id,
            dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _direccionPersonaService.EliminarAsync(id);

        return NoContent();
    }
}