using MagisAuto.Application.DTOs.Pago;

namespace MagisAuto.Application.Interfaces.Pago;

public interface IPagoRepository
{
    Task<IEnumerable<PagoDto>> ObtenerTodosAsync();
    Task<IEnumerable<PagoDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearPagoDto dto);
    Task<bool> ActualizarAsync(int pagoId, ActualizarPagoDto dto);
    Task<bool> EliminarAsync(int pagoId);
}