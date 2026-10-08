using MagisAuto.Application.DTOs.DireccionPersona;

namespace MagisAuto.Application.Interfaces.DireccionPersona;

public interface IDireccionPersonaRepository
{
    Task<IEnumerable<DireccionPersonaDto>> ObtenerTodosAsync();
    Task<IEnumerable<DireccionPersonaDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearDireccionPersonaDto dto);
    Task<bool> ActualizarAsync(
        int direccionPersonaId,
        ActualizarDireccionPersonaDto dto);
    Task<bool> EliminarAsync(int direccionPersonaId);
}