using MagisAuto.Application.DTOs.TelefonoReferencia;

namespace MagisAuto.Application.Interfaces.TelefonoReferencia;

public interface ITelefonoReferenciaService
{
    Task<IEnumerable<TelefonoReferenciaDto>> ObtenerTodosAsync();
    Task<IEnumerable<TelefonoReferenciaDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearTelefonoReferenciaDto dto);
    Task<bool> ActualizarAsync(
        int telefonoReferenciaId,
        ActualizarTelefonoReferenciaDto dto);
    Task<bool> EliminarAsync(int telefonoReferenciaId);
}