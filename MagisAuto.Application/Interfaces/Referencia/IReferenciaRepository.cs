using MagisAuto.Application.DTOs.Referencia;

namespace MagisAuto.Application.Interfaces.Referencia;

public interface IReferenciaRepository
{
    Task<IEnumerable<ReferenciaDto>> ObtenerTodosAsync();
    Task<IEnumerable<ReferenciaDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearReferenciaDto dto);
    Task<bool> ActualizarAsync(
        int referenciaId,
        ActualizarReferenciaDto dto);
    Task<bool> EliminarAsync(int referenciaId);
}