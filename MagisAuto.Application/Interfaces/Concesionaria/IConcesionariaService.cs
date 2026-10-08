using MagisAuto.Application.DTOs.Concesionaria;

namespace MagisAuto.Application.Interfaces.Concesionaria;

public interface IConcesionariaService
{
    Task<IEnumerable<ConcesionariaDto>> ObtenerTodosAsync();

    Task<IEnumerable<ConcesionariaDto>> BuscarAsync(
        string? criterio);

    Task<int> CrearAsync(
        CrearConcesionariaDto dto);

    Task<bool> ActualizarAsync(
        int concesionariaId,
        ActualizarConcesionariaDto dto);

    Task<bool> EliminarAsync(
        int concesionariaId);
}