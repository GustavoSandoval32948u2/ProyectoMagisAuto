using MagisAuto.Application.DTOs.EstadoCredito;
using MagisAuto.Application.Interfaces.EstadoCredito;

namespace MagisAuto.Application.Services;

public class EstadoCreditoService : IEstadoCreditoService
{
    private readonly IEstadoCreditoRepository _estadoCreditoRepository;

    public EstadoCreditoService(
        IEstadoCreditoRepository estadoCreditoRepository)
    {
        _estadoCreditoRepository = estadoCreditoRepository;
    }

    public async Task<IEnumerable<EstadoCreditoDto>> ObtenerTodosAsync()
    {
        return await _estadoCreditoRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<EstadoCreditoDto>> BuscarAsync(
        string? criterio)
    {
        return await _estadoCreditoRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearEstadoCreditoDto dto)
    {
        return await _estadoCreditoRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int estadoCreditoId,
        ActualizarEstadoCreditoDto dto)
    {
        return await _estadoCreditoRepository.ActualizarAsync(
            estadoCreditoId,
            dto);
    }

    public async Task<bool> EliminarAsync(int estadoCreditoId)
    {
        return await _estadoCreditoRepository.EliminarAsync(
            estadoCreditoId);
    }
}