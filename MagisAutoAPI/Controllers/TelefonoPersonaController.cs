using MagisAuto.Application.DTOs.TelefonoPersona;
using MagisAuto.Application.Interfaces.TelefonoPersona;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelefonoPersonaController : ControllerBase
{
    private readonly ITelefonoPersonaService _telefonoPersonaService;

    public TelefonoPersonaController(
        ITelefonoPersonaService telefonoPersonaService)
    {
        _telefonoPersonaService = telefonoPersonaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TelefonoPersonaDto>>>
        ObtenerTodos()
    {
        var telefonos =
            await _telefonoPersonaService.ObtenerTodosAsync();

        return Ok(telefonos);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<TelefonoPersonaDto>>>
        Buscar([FromQuery] string? criterio)
    {
        var telefonos =
            await _telefonoPersonaService.BuscarAsync(criterio);

        return Ok(telefonos);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(
        CrearTelefonoPersonaDto dto)
    {
        var telefonoPersonaId =
            await _telefonoPersonaService.CrearAsync(dto);

        return Ok(telefonoPersonaId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarTelefonoPersonaDto dto)
    {
        await _telefonoPersonaService.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _telefonoPersonaService.EliminarAsync(id);

        return NoContent();
    }
}