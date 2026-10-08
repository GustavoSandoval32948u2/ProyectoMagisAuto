using MagisAuto.Application.DTOs.HistorialDepreciacion;

namespace MagisAuto.Application.Interfaces.HistorialDepreciacion;

public interface IHistorialDepreciacionService
{
    Task<IEnumerable<HistorialDepreciacionDto>> ObtenerTodosAsync();

    Task<IEnumerable<HistorialDepreciacionDto>> BuscarAsync(
        string? criterio);

    Task<int> CrearAsync(
        CrearHistorialDepreciacionDto dto);

    Task<bool> ActualizarAsync(
        int historialDepreciacionId,
        ActualizarHistorialDepreciacionDto dto);

    Task<bool> EliminarAsync(
        int historialDepreciacionId);
}