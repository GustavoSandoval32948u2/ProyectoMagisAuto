using MagisAuto.Application.DTOs.DireccionReferencia;
using MagisAuto.Application.Interfaces.DireccionReferencia;

namespace MagisAuto.Application.Services;

public class DireccionReferenciaService : IDireccionReferenciaService
{
    private readonly IDireccionReferenciaRepository _repository;

    public DireccionReferenciaService(
        IDireccionReferenciaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DireccionReferenciaDto>> ObtenerTodosAsync()
    {
        return await _repository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<DireccionReferenciaDto>> BuscarAsync(
        string? criterio)
    {
        return await _repository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearDireccionReferenciaDto dto)
    {
        return await _repository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int direccionReferenciaId,
        ActualizarDireccionReferenciaDto dto)
    {
        return await _repository.ActualizarAsync(
            direccionReferenciaId,
            dto);
    }

    public async Task<bool> EliminarAsync(int direccionReferenciaId)
    {
        return await _repository.EliminarAsync(direccionReferenciaId);
    }
}