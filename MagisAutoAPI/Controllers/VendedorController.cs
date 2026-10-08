using Microsoft.AspNetCore.Mvc;
using MagisAuto.Application.DTOs.Vendedor;
using MagisAuto.Application.Interfaces.Vendedor;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendedorController : ControllerBase
{
    private readonly IVendedorService _service;

    public VendedorController(
        IVendedorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var resultado = await _service.ObtenerTodosAsync();

        return Ok(resultado);
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar(
        [FromQuery] string? criterio)
    {
        var resultado = await _service.BuscarAsync(criterio);

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearVendedorDto dto)
    {
        var id = await _service.CrearAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] ActualizarVendedorDto dto)
    {
        await _service.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _service.EliminarAsync(id);

        return NoContent();
    }
}