using MagisAuto.Application.DTOs.Garantia;

namespace MagisAuto.Application.Interfaces.Garantia;

public interface IGarantiaRepository
{
    Task<IEnumerable<GarantiaDto>> ObtenerTodosAsync();
    Task<IEnumerable<GarantiaDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearGarantiaDto dto);
    Task<bool> ActualizarAsync(int garantiaId, ActualizarGarantiaDto dto);
    Task<bool> EliminarAsync(int garantiaId);
}