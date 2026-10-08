using MagisAuto.Application.DTOs.CreditoFiador;
using MagisAuto.Application.Interfaces.CreditoFiador;

namespace MagisAuto.Application.Services;

public class CreditoFiadorService : ICreditoFiadorService
{
    private readonly ICreditoFiadorRepository _creditoFiadorRepository;

    public CreditoFiadorService(ICreditoFiadorRepository creditoFiadorRepository)
    {
        _creditoFiadorRepository = creditoFiadorRepository;
    }

    public async Task<IEnumerable<CreditoFiadorDto>> ObtenerTodosAsync()
    {
        return await _creditoFiadorRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<CreditoFiadorDto>> BuscarAsync(string? criterio)
    {
        return await _creditoFiadorRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearCreditoFiadorDto dto)
    {
        return await _creditoFiadorRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int creditoFiadorId,
        ActualizarCreditoFiadorDto dto)
    {
        return await _creditoFiadorRepository.ActualizarAsync(creditoFiadorId, dto);
    }

    public async Task<bool> EliminarAsync(int creditoFiadorId)
    {
        return await _creditoFiadorRepository.EliminarAsync(creditoFiadorId);
    }
}