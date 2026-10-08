using MagisAuto.Application.DTOs.EstadoCuota;
using MagisAuto.Application.Interfaces.EstadoCuota;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadoCuotaController : ControllerBase
{
    private readonly IEstadoCuotaService _estadoCuotaService;

    public EstadoCuotaController(
        IEstadoCuotaService estadoCuotaService)
    {
        _estadoCuotaService = estadoCuotaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoCuotaDto>>> ObtenerTodos()
    {
        return Ok(await _estadoCuotaService.ObtenerTodosAsync());
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<EstadoCuotaDto>>> Buscar(
        [FromQuery] string? criterio)
    {
        return Ok(await _estadoCuotaService.BuscarAsync(criterio));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(
        CrearEstadoCuotaDto dto)
    {
        var id = await _estadoCuotaService.CrearAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarEstadoCuotaDto dto)
    {
        await _estadoCuotaService.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _estadoCuotaService.EliminarAsync(id);

        return NoContent();
    }
}