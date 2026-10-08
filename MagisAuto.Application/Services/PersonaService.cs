using MagisAuto.Application.DTOs.Persona;
using MagisAuto.Application.Interfaces.Persona;

namespace MagisAuto.Application.Services;

public class PersonaService : IPersonaService
{
    private readonly IPersonaRepository _personaRepository;

    public PersonaService(IPersonaRepository personaRepository)
    {
        _personaRepository = personaRepository;
    }

    public async Task<IEnumerable<PersonaDto>> ObtenerTodosAsync()
    {
        return await _personaRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<PersonaDto>> BuscarAsync(string? criterio)
    {
        return await _personaRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearPersonaDto dto)
    {
        return await _personaRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int personaId,
        ActualizarPersonaDto dto)
    {
        return await _personaRepository.ActualizarAsync(
            personaId,
            dto);
    }

    public async Task<bool> EliminarAsync(int personaId)
    {
        return await _personaRepository.EliminarAsync(personaId);
    }
}