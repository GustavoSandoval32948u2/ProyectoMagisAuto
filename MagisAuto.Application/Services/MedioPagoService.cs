using MagisAuto.Application.DTOs.MedioPago;
using MagisAuto.Application.Interfaces.MedioPago;

namespace MagisAuto.Application.Services;

public class MedioPagoService : IMedioPagoService
{
    private readonly IMedioPagoRepository _medioPagoRepository;

    public MedioPagoService(IMedioPagoRepository medioPagoRepository)
    {
        _medioPagoRepository = medioPagoRepository;
    }

    public async Task<IEnumerable<MedioPagoDto>> ObtenerTodosAsync()
    {
        return await _medioPagoRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<MedioPagoDto>> BuscarAsync(string? criterio)
    {
        return await _medioPagoRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearMedioPagoDto dto)
    {
        return await _medioPagoRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int medioPagoId,
        ActualizarMedioPagoDto dto)
    {
        return await _medioPagoRepository.ActualizarAsync(
            medioPagoId,
            dto);
    }

    public async Task<bool> EliminarAsync(int medioPagoId)
    {
        return await _medioPagoRepository.EliminarAsync(medioPagoId);
    }
}