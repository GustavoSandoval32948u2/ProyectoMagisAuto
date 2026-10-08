using MagisAuto.Application.DTOs.ParametroFinanciero;

namespace MagisAuto.Application.Interfaces.ParametroFinanciero;

public interface IParametroFinancieroRepository
{
    Task<IEnumerable<ParametroFinancieroDto>> ObtenerTodosAsync();
    Task<IEnumerable<ParametroFinancieroDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearParametroFinancieroDto dto);
    Task<bool> ActualizarAsync(
        int parametroFinancieroId,
        ActualizarParametroFinancieroDto dto);
    Task<bool> EliminarAsync(int parametroFinancieroId);
}