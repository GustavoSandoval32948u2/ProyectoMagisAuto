using MagisAuto.Application.DTOs.DireccionReferencia;

namespace MagisAuto.Application.Interfaces.DireccionReferencia;

public interface IDireccionReferenciaService
{
    Task<IEnumerable<DireccionReferenciaDto>> ObtenerTodosAsync();
    Task<IEnumerable<DireccionReferenciaDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearDireccionReferenciaDto dto);
    Task<bool> ActualizarAsync(
        int direccionReferenciaId,
        ActualizarDireccionReferenciaDto dto);
    Task<bool> EliminarAsync(int direccionReferenciaId);
}