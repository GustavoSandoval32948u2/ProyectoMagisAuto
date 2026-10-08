using MagisAuto.Application.DTOs.Pago;
using MagisAuto.Application.Interfaces.Pago;

namespace MagisAuto.Application.Services;

public class PagoService : IPagoService
{
    private readonly IPagoRepository _pagoRepository;

    public PagoService(IPagoRepository pagoRepository)
    {
        _pagoRepository = pagoRepository;
    }

    public async Task<IEnumerable<PagoDto>> ObtenerTodosAsync()
    {
        return await _pagoRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<PagoDto>> BuscarAsync(string? criterio)
    {
        return await _pagoRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearPagoDto dto)
    {
        return await _pagoRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int pagoId,
        ActualizarPagoDto dto)
    {
        return await _pagoRepository.ActualizarAsync(pagoId, dto);
    }

    public async Task<bool> EliminarAsync(int pagoId)
    {
        return await _pagoRepository.EliminarAsync(pagoId);
    }
}