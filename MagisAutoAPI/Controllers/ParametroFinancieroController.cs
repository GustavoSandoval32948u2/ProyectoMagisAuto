using MagisAuto.Application.DTOs.ParametroFinanciero;
using MagisAuto.Application.Interfaces.ParametroFinanciero;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParametroFinancieroController : ControllerBase
{
    private readonly IParametroFinancieroService _parametroFinancieroService;

    public ParametroFinancieroController(
        IParametroFinancieroService parametroFinancieroService)
    {
        _parametroFinancieroService = parametroFinancieroService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParametroFinancieroDto>>>
        ObtenerTodos()
    {
        return Ok(
            await _parametroFinancieroService.ObtenerTodosAsync());
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<ParametroFinancieroDto>>> Buscar(
        [FromQuery] string? criterio)
    {
        return Ok(
            await _parametroFinancieroService.BuscarAsync(criterio));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(
        CrearParametroFinancieroDto dto)
    {
        var id = await _parametroFinancieroService.CrearAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarParametroFinancieroDto dto)
    {
        await _parametroFinancieroService.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _parametroFinancieroService.EliminarAsync(id);

        return NoContent();
    }
}