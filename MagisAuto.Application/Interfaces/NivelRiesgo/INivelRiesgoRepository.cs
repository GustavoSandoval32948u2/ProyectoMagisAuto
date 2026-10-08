using MagisAuto.Application.DTOs.NivelRiesgo;

namespace MagisAuto.Application.Interfaces.NivelRiesgo;

public interface INivelRiesgoRepository
{
    Task<IEnumerable<NivelRiesgoDto>> ObtenerTodosAsync();
    Task<IEnumerable<NivelRiesgoDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearNivelRiesgoDto dto);
    Task<bool> ActualizarAsync(
        int nivelRiesgoId,
        ActualizarNivelRiesgoDto dto);
    Task<bool> EliminarAsync(int nivelRiesgoId);
}