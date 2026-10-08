using MagisAuto.Application.DTOs.Persona;

namespace MagisAuto.Application.Interfaces.Persona;

public interface IPersonaRepository
{
    Task<IEnumerable<PersonaDto>> ObtenerTodosAsync();
    Task<IEnumerable<PersonaDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearPersonaDto dto);
    Task<bool> ActualizarAsync(
        int personaId,
        ActualizarPersonaDto dto);
    Task<bool> EliminarAsync(int personaId);
}