using MagisAuto.Application.DTOs.TipoGarantia;

namespace MagisAuto.Application.Interfaces.TipoGarantia;

public interface ITipoGarantiaService
{
    Task<IEnumerable<TipoGarantiaDto>> ObtenerTodosAsync();
    Task<IEnumerable<TipoGarantiaDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearTipoGarantiaDto dto);
    Task<bool> ActualizarAsync(
        int tipoGarantiaId,
        ActualizarTipoGarantiaDto dto);
    Task<bool> EliminarAsync(int tipoGarantiaId);
}