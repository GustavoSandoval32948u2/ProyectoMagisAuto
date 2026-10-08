using MagisAuto.Application.DTOs.Referencia;
using MagisAuto.Application.Interfaces.Referencia;

namespace MagisAuto.Application.Services;

public class ReferenciaService : IReferenciaService
{
    private readonly IReferenciaRepository _referenciaRepository;

    public ReferenciaService(
        IReferenciaRepository referenciaRepository)
    {
        _referenciaRepository = referenciaRepository;
    }

    public async Task<IEnumerable<ReferenciaDto>> ObtenerTodosAsync()
    {
        return await _referenciaRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<ReferenciaDto>> BuscarAsync(
        string? criterio)
    {
        return await _referenciaRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearReferenciaDto dto)
    {
        return await _referenciaRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int referenciaId,
        ActualizarReferenciaDto dto)
    {
        return await _referenciaRepository.ActualizarAsync(
            referenciaId,
            dto);
    }

    public async Task<bool> EliminarAsync(int referenciaId)
    {
        return await _referenciaRepository.EliminarAsync(referenciaId);
    }
}