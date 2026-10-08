using MagisAuto.Application.DTOs.Pago;
using MagisAuto.Application.Interfaces.Pago;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagoController : ControllerBase
{
    private readonly IPagoService _pagoService;

    public PagoController(IPagoService pagoService)
    {
        _pagoService = pagoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        return Ok(await _pagoService.ObtenerTodosAsync());
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar([FromQuery] string? criterio)
    {
        return Ok(await _pagoService.BuscarAsync(criterio));
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearPagoDto dto)
    {
        var id = await _pagoService.CrearAsync(dto);
        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] ActualizarPagoDto dto)
    {
        await _pagoService.ActualizarAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _pagoService.EliminarAsync(id);
        return NoContent();
    }
}