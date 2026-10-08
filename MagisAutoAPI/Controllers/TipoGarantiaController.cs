using MagisAuto.Application.DTOs.TipoGarantia;
using MagisAuto.Application.Interfaces.TipoGarantia;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoGarantiaController : ControllerBase
{
    private readonly ITipoGarantiaService _tipoGarantiaService;

    public TipoGarantiaController(
        ITipoGarantiaService tipoGarantiaService)
    {
        _tipoGarantiaService = tipoGarantiaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoGarantiaDto>>> ObtenerTodos()
    {
        return Ok(await _tipoGarantiaService.ObtenerTodosAsync());
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<TipoGarantiaDto>>> Buscar(
        [FromQuery] string? criterio)
    {
        return Ok(await _tipoGarantiaService.BuscarAsync(criterio));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(
        CrearTipoGarantiaDto dto)
    {
        var id = await _tipoGarantiaService.CrearAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarTipoGarantiaDto dto)
    {
        await _tipoGarantiaService.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _tipoGarantiaService.EliminarAsync(id);

        return NoContent();
    }
}