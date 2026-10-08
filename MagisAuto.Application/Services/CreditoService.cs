using MagisAuto.Application.DTOs.Credito;
using MagisAuto.Application.Interfaces.Credito;

namespace MagisAuto.Application.Services;

public class CreditoService : ICreditoService
{
    private readonly ICreditoRepository _repository;

    public CreditoService(
        ICreditoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CreditoDto>> ObtenerTodosAsync()
    {
        return await _repository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<CreditoDto>> BuscarAsync(
        string? criterio)
    {
        return await _repository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(
        CrearCreditoDto dto)
    {
        return await _repository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int creditoId,
        ActualizarCreditoDto dto)
    {
        return await _repository.ActualizarAsync(
            creditoId,
            dto);
    }

    public async Task<bool> EliminarAsync(
        int creditoId)
    {
        return await _repository.EliminarAsync(creditoId);
    }
}