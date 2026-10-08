using MagisAuto.Application.DTOs.Garantia;
using MagisAuto.Application.Interfaces.Garantia;

namespace MagisAuto.Application.Services;

public class GarantiaService : IGarantiaService
{
    private readonly IGarantiaRepository _garantiaRepository;

    public GarantiaService(IGarantiaRepository garantiaRepository)
    {
        _garantiaRepository = garantiaRepository;
    }

    public async Task<IEnumerable<GarantiaDto>> ObtenerTodosAsync()
    {
        return await _garantiaRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<GarantiaDto>> BuscarAsync(string? criterio)
    {
        return await _garantiaRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearGarantiaDto dto)
    {
        return await _garantiaRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int garantiaId,
        ActualizarGarantiaDto dto)
    {
        return await _garantiaRepository.ActualizarAsync(garantiaId, dto);
    }

    public async Task<bool> EliminarAsync(int garantiaId)
    {
        return await _garantiaRepository.EliminarAsync(garantiaId);
    }
}