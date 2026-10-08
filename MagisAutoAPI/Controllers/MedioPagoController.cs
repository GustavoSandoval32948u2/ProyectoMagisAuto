using MagisAuto.Application.DTOs.MedioPago;
using MagisAuto.Application.Interfaces.MedioPago;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedioPagoController : ControllerBase
{
    private readonly IMedioPagoService _medioPagoService;

    public MedioPagoController(IMedioPagoService medioPagoService)
    {
        _medioPagoService = medioPagoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedioPagoDto>>> ObtenerTodos()
    {
        return Ok(await _medioPagoService.ObtenerTodosAsync());
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<MedioPagoDto>>> Buscar(
        [FromQuery] string? criterio)
    {
        return Ok(await _medioPagoService.BuscarAsync(criterio));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(CrearMedioPagoDto dto)
    {
        var id = await _medioPagoService.CrearAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarMedioPagoDto dto)
    {
        await _medioPagoService.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _medioPagoService.EliminarAsync(id);

        return NoContent();
    }
}