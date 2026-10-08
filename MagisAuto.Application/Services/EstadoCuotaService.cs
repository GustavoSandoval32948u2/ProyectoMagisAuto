using MagisAuto.Application.DTOs.EstadoCuota;
using MagisAuto.Application.Interfaces.EstadoCuota;

namespace MagisAuto.Application.Services;

public class EstadoCuotaService : IEstadoCuotaService
{
    private readonly IEstadoCuotaRepository _estadoCuotaRepository;

    public EstadoCuotaService(
        IEstadoCuotaRepository estadoCuotaRepository)
    {
        _estadoCuotaRepository = estadoCuotaRepository;
    }

    public async Task<IEnumerable<EstadoCuotaDto>> ObtenerTodosAsync()
    {
        return await _estadoCuotaRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<EstadoCuotaDto>> BuscarAsync(string? criterio)
    {
        return await _estadoCuotaRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearEstadoCuotaDto dto)
    {
        return await _estadoCuotaRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int estadoCuotaId,
        ActualizarEstadoCuotaDto dto)
    {
        return await _estadoCuotaRepository.ActualizarAsync(
            estadoCuotaId,
            dto);
    }

    public async Task<bool> EliminarAsync(int estadoCuotaId)
    {
        return await _estadoCuotaRepository.EliminarAsync(estadoCuotaId);
    }
}