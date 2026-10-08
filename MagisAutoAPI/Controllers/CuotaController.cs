using MagisAuto.Application.DTOs.Cuota;
using MagisAuto.Application.Interfaces.Cuota;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuotaController : ControllerBase
{
    private readonly ICuotaService _cuotaService;

    public CuotaController(ICuotaService cuotaService)
    {
        _cuotaService = cuotaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        return Ok(await _cuotaService.ObtenerTodosAsync());
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar([FromQuery] string? criterio)
    {
        return Ok(await _cuotaService.BuscarAsync(criterio));
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearCuotaDto dto)
    {
        var id = await _cuotaService.CrearAsync(dto);
        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] ActualizarCuotaDto dto)
    {
        await _cuotaService.ActualizarAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _cuotaService.EliminarAsync(id);
        return NoContent();
    }
}