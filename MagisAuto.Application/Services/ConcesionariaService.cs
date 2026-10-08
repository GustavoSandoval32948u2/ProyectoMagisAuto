using MagisAuto.Application.DTOs.Concesionaria;
using MagisAuto.Application.Interfaces.Concesionaria;

namespace MagisAuto.Application.Services;

public class ConcesionariaService : IConcesionariaService
{
    private readonly IConcesionariaRepository _repository;

    public ConcesionariaService(
        IConcesionariaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ConcesionariaDto>> ObtenerTodosAsync()
    {
        return await _repository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<ConcesionariaDto>> BuscarAsync(
        string? criterio)
    {
        return await _repository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(
        CrearConcesionariaDto dto)
    {
        return await _repository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int concesionariaId,
        ActualizarConcesionariaDto dto)
    {
        return await _repository.ActualizarAsync(
            concesionariaId,
            dto);
    }

    public async Task<bool> EliminarAsync(
        int concesionariaId)
    {
        return await _repository.EliminarAsync(
            concesionariaId);
    }
}