using MagisAuto.Application.DTOs.ParametroFinanciero;
using MagisAuto.Application.Interfaces.ParametroFinanciero;

namespace MagisAuto.Application.Services;

public class ParametroFinancieroService : IParametroFinancieroService
{
    private readonly IParametroFinancieroRepository _parametroFinancieroRepository;

    public ParametroFinancieroService(
        IParametroFinancieroRepository parametroFinancieroRepository)
    {
        _parametroFinancieroRepository = parametroFinancieroRepository;
    }

    public async Task<IEnumerable<ParametroFinancieroDto>> ObtenerTodosAsync()
    {
        return await _parametroFinancieroRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<ParametroFinancieroDto>> BuscarAsync(
        string? criterio)
    {
        return await _parametroFinancieroRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearParametroFinancieroDto dto)
    {
        return await _parametroFinancieroRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int parametroFinancieroId,
        ActualizarParametroFinancieroDto dto)
    {
        return await _parametroFinancieroRepository.ActualizarAsync(
            parametroFinancieroId,
            dto);
    }

    public async Task<bool> EliminarAsync(int parametroFinancieroId)
    {
        return await _parametroFinancieroRepository.EliminarAsync(
            parametroFinancieroId);
    }
}