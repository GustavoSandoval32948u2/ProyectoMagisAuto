using MagisAuto.Application.DTOs.EstadoCredito;
using MagisAuto.Application.Interfaces.EstadoCredito;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadoCreditoController : ControllerBase
{
    private readonly IEstadoCreditoService _estadoCreditoService;

    public EstadoCreditoController(
        IEstadoCreditoService estadoCreditoService)
    {
        _estadoCreditoService = estadoCreditoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoCreditoDto>>>
        ObtenerTodos()
    {
        var estados = await _estadoCreditoService.ObtenerTodosAsync();

        return Ok(estados);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<EstadoCreditoDto>>>
        Buscar([FromQuery] string? criterio)
    {
        var estados = await _estadoCreditoService.BuscarAsync(criterio);

        return Ok(estados);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(
        CrearEstadoCreditoDto dto)
    {
        var estadoCreditoId =
            await _estadoCreditoService.CrearAsync(dto);

        return Ok(estadoCreditoId);
    }

    [HttpPut("{estadoCreditoId:int}")]
    public async Task<IActionResult> Actualizar(
        int estadoCreditoId,
        ActualizarEstadoCreditoDto dto)
    {
        await _estadoCreditoService.ActualizarAsync(
            estadoCreditoId,
            dto);

        return NoContent();
    }

    [HttpDelete("{estadoCreditoId:int}")]
    public async Task<IActionResult> Eliminar(
        int estadoCreditoId)
    {
        await _estadoCreditoService.EliminarAsync(
            estadoCreditoId);

        return NoContent();
    }
}