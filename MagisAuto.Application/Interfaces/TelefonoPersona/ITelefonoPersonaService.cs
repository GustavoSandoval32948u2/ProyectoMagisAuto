using MagisAuto.Application.DTOs.TelefonoPersona;

namespace MagisAuto.Application.Interfaces.TelefonoPersona;

public interface ITelefonoPersonaService
{
    Task<IEnumerable<TelefonoPersonaDto>> ObtenerTodosAsync();
    Task<IEnumerable<TelefonoPersonaDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearTelefonoPersonaDto dto);
    Task<bool> ActualizarAsync(
        int telefonoPersonaId,
        ActualizarTelefonoPersonaDto dto);
    Task<bool> EliminarAsync(int telefonoPersonaId);
}