using MagisAuto.Application.DTOs.Credito;

namespace MagisAuto.Application.Interfaces.Credito;

public interface ICreditoRepository
{
    Task<IEnumerable<CreditoDto>> ObtenerTodosAsync();

    Task<IEnumerable<CreditoDto>> BuscarAsync(
        string? criterio);

    Task<int> CrearAsync(
        CrearCreditoDto dto);

    Task<bool> ActualizarAsync(
        int creditoId,
        ActualizarCreditoDto dto);

    Task<bool> EliminarAsync(
        int creditoId);
}