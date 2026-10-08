using MagisAuto.Application.DTOs.TipoGarantia;
using MagisAuto.Application.Interfaces.TipoGarantia;

namespace MagisAuto.Application.Services;

public class TipoGarantiaService : ITipoGarantiaService
{
    private readonly ITipoGarantiaRepository _tipoGarantiaRepository;

    public TipoGarantiaService(
        ITipoGarantiaRepository tipoGarantiaRepository)
    {
        _tipoGarantiaRepository = tipoGarantiaRepository;
    }

    public async Task<IEnumerable<TipoGarantiaDto>> ObtenerTodosAsync()
    {
        return await _tipoGarantiaRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<TipoGarantiaDto>> BuscarAsync(string? criterio)
    {
        return await _tipoGarantiaRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearTipoGarantiaDto dto)
    {
        return await _tipoGarantiaRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int tipoGarantiaId,
        ActualizarTipoGarantiaDto dto)
    {
        return await _tipoGarantiaRepository.ActualizarAsync(
            tipoGarantiaId,
            dto);
    }

    public async Task<bool> EliminarAsync(int tipoGarantiaId)
    {
        return await _tipoGarantiaRepository.EliminarAsync(tipoGarantiaId);
    }
}