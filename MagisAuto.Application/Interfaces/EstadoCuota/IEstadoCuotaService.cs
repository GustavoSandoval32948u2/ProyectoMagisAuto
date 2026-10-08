using MagisAuto.Application.DTOs.EstadoCuota;

namespace MagisAuto.Application.Interfaces.EstadoCuota;

public interface IEstadoCuotaService
{
    Task<IEnumerable<EstadoCuotaDto>> ObtenerTodosAsync();
    Task<IEnumerable<EstadoCuotaDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearEstadoCuotaDto dto);
    Task<bool> ActualizarAsync(int estadoCuotaId, ActualizarEstadoCuotaDto dto);
    Task<bool> EliminarAsync(int estadoCuotaId);
}