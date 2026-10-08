using MagisAuto.Application.DTOs.Garantia;
using MagisAuto.Application.Interfaces.Garantia;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GarantiaController : ControllerBase
{
    private readonly IGarantiaService _garantiaService;

    public GarantiaController(IGarantiaService garantiaService)
    {
        _garantiaService = garantiaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        return Ok(await _garantiaService.ObtenerTodosAsync());
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar([FromQuery] string? criterio)
    {
        return Ok(await _garantiaService.BuscarAsync(criterio));
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearGarantiaDto dto)
    {
        var id = await _garantiaService.CrearAsync(dto);
        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] ActualizarGarantiaDto dto)
    {
        await _garantiaService.ActualizarAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _garantiaService.EliminarAsync(id);
        return NoContent();
    }
}