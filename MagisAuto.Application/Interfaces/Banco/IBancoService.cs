using MagisAuto.Application.DTOs.Banco;

namespace MagisAuto.Application.Interfaces.Banco;

public interface IBancoService
{
    Task<IEnumerable<BancoDto>> ObtenerTodosAsync();
    Task<IEnumerable<BancoDto>> BuscarAsync(string? criterio);
    Task<int> CrearAsync(CrearBancoDto dto);
    Task<bool> ActualizarAsync(int bancoId, ActualizarBancoDto dto);
    Task<bool> EliminarAsync(int bancoId);
}