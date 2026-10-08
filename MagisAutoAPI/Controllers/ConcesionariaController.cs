using Microsoft.AspNetCore.Mvc;
using MagisAuto.Application.DTOs.Concesionaria;
using MagisAuto.Application.Interfaces.Concesionaria;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConcesionariaController : ControllerBase
{
    private readonly IConcesionariaService _service;

    public ConcesionariaController(
        IConcesionariaService service)
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
        [FromBody] CrearConcesionariaDto dto)
    {
        var id = await _service.CrearAsync(dto);

        return Ok(id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] ActualizarConcesionariaDto dto)
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