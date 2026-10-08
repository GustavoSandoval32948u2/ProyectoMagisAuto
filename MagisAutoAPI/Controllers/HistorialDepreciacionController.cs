using Microsoft.AspNetCore.Mvc;
using MagisAuto.Application.DTOs.HistorialDepreciacion;
using MagisAuto.Application.Interfaces.HistorialDepreciacion;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistorialDepreciacionController : ControllerBase
{
    private readonly IHistorialDepreciacionService _service;

    public HistorialDepreciacionController(
        IHistorialDepreciacionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var resultado = await _service.ObtenerTodosAsync();

        return Ok(resultado);
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar(
        [FromQuery] string? criterio)
    {
        var resultado = await _service.BuscarAsync(criterio);

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearHistorialDepreciacionDto dto)
    {
        var id = await _service.CrearAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] ActualizarHistorialDepreciacionDto dto)
    {
        await _service.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _service.EliminarAsync(id);

        return NoContent();
    }
}