using MagisAuto.Application.DTOs.NivelRiesgo;
using MagisAuto.Application.Interfaces.NivelRiesgo;

namespace MagisAuto.Application.Services;

public class NivelRiesgoService : INivelRiesgoService
{
    private readonly INivelRiesgoRepository _nivelRiesgoRepository;

    public NivelRiesgoService(
        INivelRiesgoRepository nivelRiesgoRepository)
    {
        _nivelRiesgoRepository = nivelRiesgoRepository;
    }

    public async Task<IEnumerable<NivelRiesgoDto>> ObtenerTodosAsync()
    {
        return await _nivelRiesgoRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<NivelRiesgoDto>> BuscarAsync(string? criterio)
    {
        return await _nivelRiesgoRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearNivelRiesgoDto dto)
    {
        return await _nivelRiesgoRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int nivelRiesgoId,
        ActualizarNivelRiesgoDto dto)
    {
        return await _nivelRiesgoRepository.ActualizarAsync(
            nivelRiesgoId,
            dto);
    }

    public async Task<bool> EliminarAsync(int nivelRiesgoId)
    {
        return await _nivelRiesgoRepository.EliminarAsync(nivelRiesgoId);
    }
}