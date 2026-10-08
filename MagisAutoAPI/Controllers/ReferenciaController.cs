using MagisAuto.Application.DTOs.Referencia;
using MagisAuto.Application.Interfaces.Referencia;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReferenciaController : ControllerBase
{
    private readonly IReferenciaService _referenciaService;

    public ReferenciaController(
        IReferenciaService referenciaService)
    {
        _referenciaService = referenciaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReferenciaDto>>>
        ObtenerTodos()
    {
        var referencias =
            await _referenciaService.ObtenerTodosAsync();

        return Ok(referencias);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<ReferenciaDto>>>
        Buscar([FromQuery] string? criterio)
    {
        var referencias =
            await _referenciaService.BuscarAsync(criterio);

        return Ok(referencias);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(
        CrearReferenciaDto dto)
    {
        var referenciaId =
            await _referenciaService.CrearAsync(dto);

        return Ok(referenciaId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarReferenciaDto dto)
    {
        await _referenciaService.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _referenciaService.EliminarAsync(id);

        return NoContent();
    }
}