using MagisAuto.Application.DTOs.Cuota;
using MagisAuto.Application.Interfaces.Cuota;

namespace MagisAuto.Application.Services;

public class CuotaService : ICuotaService
{
    private readonly ICuotaRepository _cuotaRepository;

    public CuotaService(ICuotaRepository cuotaRepository)
    {
        _cuotaRepository = cuotaRepository;
    }

    public async Task<IEnumerable<CuotaDto>> ObtenerTodosAsync()
    {
        return await _cuotaRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<CuotaDto>> BuscarAsync(string? criterio)
    {
        return await _cuotaRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearCuotaDto dto)
    {
        return await _cuotaRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int cuotaId,
        ActualizarCuotaDto dto)
    {
        return await _cuotaRepository.ActualizarAsync(cuotaId, dto);
    }

    public async Task<bool> EliminarAsync(int cuotaId)
    {
        return await _cuotaRepository.EliminarAsync(cuotaId);
    }
}