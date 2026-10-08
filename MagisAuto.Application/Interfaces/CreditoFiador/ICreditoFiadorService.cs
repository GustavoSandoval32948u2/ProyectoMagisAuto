using MagisAuto.Application.DTOs.CreditoFiador;

namespace MagisAuto.Application.Interfaces.CreditoFiador;

public interface ICreditoFiadorService
{
    Task<IEnumerable<CreditoFiadorDto>> ObtenerTodosAsync();
    Task<IEnumerable<CreditoFiadorDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearCreditoFiadorDto dto);
    Task<bool> ActualizarAsync(int creditoFiadorId, ActualizarCreditoFiadorDto dto);
    Task<bool> EliminarAsync(int creditoFiadorId);
}