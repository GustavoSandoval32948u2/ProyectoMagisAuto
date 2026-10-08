using MagisAuto.Application.DTOs.TelefonoReferencia;
using MagisAuto.Application.Interfaces.TelefonoReferencia;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelefonoReferenciaController : ControllerBase
{
    private readonly ITelefonoReferenciaService _telefonoReferenciaService;

    public TelefonoReferenciaController(
        ITelefonoReferenciaService telefonoReferenciaService)
    {
        _telefonoReferenciaService = telefonoReferenciaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TelefonoReferenciaDto>>>
        ObtenerTodos()
    {
        var telefonos =
            await _telefonoReferenciaService.ObtenerTodosAsync();

        return Ok(telefonos);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<TelefonoReferenciaDto>>>
        Buscar([FromQuery] string? criterio)
    {
        var telefonos =
            await _telefonoReferenciaService.BuscarAsync(criterio);

        return Ok(telefonos);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(
        CrearTelefonoReferenciaDto dto)
    {
        var telefonoReferenciaId =
            await _telefonoReferenciaService.CrearAsync(dto);

        return Ok(telefonoReferenciaId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarTelefonoReferenciaDto dto)
    {
        await _telefonoReferenciaService.ActualizarAsync(
            id,
            dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _telefonoReferenciaService.EliminarAsync(id);

        return NoContent();
    }
}