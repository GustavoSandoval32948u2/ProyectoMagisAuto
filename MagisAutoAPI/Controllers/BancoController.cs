using MagisAuto.Application.DTOs.Banco;
using MagisAuto.Application.Interfaces.Banco;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BancoController : ControllerBase
{
    private readonly IBancoService _bancoService;

    public BancoController(IBancoService bancoService)
    {
        _bancoService = bancoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BancoDto>>> ObtenerTodos()
    {
        return Ok(await _bancoService.ObtenerTodosAsync());
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<BancoDto>>> Buscar(
        [FromQuery] string? criterio)
    {
        return Ok(await _bancoService.BuscarAsync(criterio));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(CrearBancoDto dto)
    {
        var id = await _bancoService.CrearAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarBancoDto dto)
    {
        await _bancoService.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _bancoService.EliminarAsync(id);

        return NoContent();
    }
}