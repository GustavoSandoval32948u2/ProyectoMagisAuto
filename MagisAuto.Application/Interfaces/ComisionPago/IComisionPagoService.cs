using MagisAuto.Application.DTOs.ComisionPago;

namespace MagisAuto.Application.Interfaces.ComisionPago;

public interface IComisionPagoService
{
    Task<IEnumerable<ComisionPagoDto>> ObtenerTodosAsync();

    Task<IEnumerable<ComisionPagoDto>> BuscarAsync(string? criterio);

    Task<int> CrearAsync(CrearComisionPagoDto dto);

    Task<bool> ActualizarAsync(
        int comisionPagoId,
        ActualizarComisionPagoDto dto);

    Task<bool> EliminarAsync(int comisionPagoId);
}