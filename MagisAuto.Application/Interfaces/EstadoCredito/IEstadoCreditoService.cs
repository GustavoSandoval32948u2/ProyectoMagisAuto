using MagisAuto.Application.DTOs.EstadoCredito;

namespace MagisAuto.Application.Interfaces.EstadoCredito;

public interface IEstadoCreditoService
{
    Task<IEnumerable<EstadoCreditoDto>> ObtenerTodosAsync();
    Task<IEnumerable<EstadoCreditoDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearEstadoCreditoDto dto);
    Task<bool> ActualizarAsync(
        int estadoCreditoId,
        ActualizarEstadoCreditoDto dto);
    Task<bool> EliminarAsync(int estadoCreditoId);
}