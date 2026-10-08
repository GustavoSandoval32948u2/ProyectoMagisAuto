using MagisAuto.Application.DTOs.CreditoFiador;
using MagisAuto.Application.Interfaces.CreditoFiador;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditoFiadorController : ControllerBase
{
    private readonly ICreditoFiadorService _creditoFiadorService;

    public CreditoFiadorController(ICreditoFiadorService creditoFiadorService)
    {
        _creditoFiadorService = creditoFiadorService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        return Ok(await _creditoFiadorService.ObtenerTodosAsync());
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar([FromQuery] string? criterio)
    {
        return Ok(await _creditoFiadorService.BuscarAsync(criterio));
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearCreditoFiadorDto dto)
    {
        var id = await _creditoFiadorService.CrearAsync(dto);
        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] ActualizarCreditoFiadorDto dto)
    {
        await _creditoFiadorService.ActualizarAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _creditoFiadorService.EliminarAsync(id);
        return NoContent();
    }
}