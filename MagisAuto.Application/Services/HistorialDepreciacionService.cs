using MagisAuto.Application.DTOs.HistorialDepreciacion;
using MagisAuto.Application.Interfaces.HistorialDepreciacion;

namespace MagisAuto.Application.Services;

public class HistorialDepreciacionService
    : IHistorialDepreciacionService
{
    private readonly IHistorialDepreciacionRepository _repository;

    public HistorialDepreciacionService(
        IHistorialDepreciacionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<HistorialDepreciacionDto>>
        ObtenerTodosAsync()
    {
        return await _repository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<HistorialDepreciacionDto>>
        BuscarAsync(string? criterio)
    {
        return await _repository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(
        CrearHistorialDepreciacionDto dto)
    {
        return await _repository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int historialDepreciacionId,
        ActualizarHistorialDepreciacionDto dto)
    {
        return await _repository.ActualizarAsync(
            historialDepreciacionId,
            dto);
    }

    public async Task<bool> EliminarAsync(
        int historialDepreciacionId)
    {
        return await _repository.EliminarAsync(
            historialDepreciacionId);
    }
}