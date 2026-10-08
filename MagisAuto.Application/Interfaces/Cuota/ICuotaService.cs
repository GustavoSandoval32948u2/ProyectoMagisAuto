using MagisAuto.Application.DTOs.Cuota;

namespace MagisAuto.Application.Interfaces.Cuota;

public interface ICuotaService
{
    Task<IEnumerable<CuotaDto>> ObtenerTodosAsync();
    Task<IEnumerable<CuotaDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearCuotaDto dto);
    Task<bool> ActualizarAsync(int cuotaId, ActualizarCuotaDto dto);
    Task<bool> EliminarAsync(int cuotaId);
}