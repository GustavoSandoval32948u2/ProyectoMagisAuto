using MagisAuto.Application.DTOs.ComisionPago;
using MagisAuto.Application.Interfaces.ComisionPago;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComisionPagoController : ControllerBase
{
    private readonly IComisionPagoService _comisionPagoService;

    public ComisionPagoController(
        IComisionPagoService comisionPagoService)
    {
        _comisionPagoService = comisionPagoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var resultado =
            await _comisionPagoService.ObtenerTodosAsync();

        return Ok(resultado);
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar(
        [FromQuery] string? criterio)
    {
        var resultado =
            await _comisionPagoService.BuscarAsync(criterio);

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearComisionPagoDto dto)
    {
        var id = await _comisionPagoService.CrearAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] ActualizarComisionPagoDto dto)
    {
        await _comisionPagoService.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _comisionPagoService.EliminarAsync(id);

        return NoContent();
    }
}