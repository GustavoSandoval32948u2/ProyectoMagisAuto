using MagisAuto.Application.DTOs.MedioPago;

namespace MagisAuto.Application.Interfaces.MedioPago;

public interface IMedioPagoRepository
{
    Task<IEnumerable<MedioPagoDto>> ObtenerTodosAsync();
    Task<IEnumerable<MedioPagoDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearMedioPagoDto dto);
    Task<bool> ActualizarAsync(int medioPagoId, ActualizarMedioPagoDto dto);
    Task<bool> EliminarAsync(int medioPagoId);
}