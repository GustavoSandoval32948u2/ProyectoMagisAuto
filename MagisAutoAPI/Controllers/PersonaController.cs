using MagisAuto.Application.DTOs.Persona;
using MagisAuto.Application.Interfaces.Persona;
using Microsoft.AspNetCore.Mvc;

namespace MagisAutoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonaController : ControllerBase
{
    private readonly IPersonaService _personaService;

    public PersonaController(IPersonaService personaService)
    {
        _personaService = personaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> ObtenerTodos()
    {
        var personas = await _personaService.ObtenerTodosAsync();

        return Ok(personas);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Buscar(
        [FromQuery] string? criterio)
    {
        var personas = await _personaService.BuscarAsync(criterio);

        return Ok(personas);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Crear(
        CrearPersonaDto dto)
    {
        var personaId = await _personaService.CrearAsync(dto);

        return Ok(personaId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ActualizarPersonaDto dto)
    {
        await _personaService.ActualizarAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _personaService.EliminarAsync(id);

        return NoContent();
    }
}