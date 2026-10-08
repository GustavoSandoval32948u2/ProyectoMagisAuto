using MagisAuto.Application.DTOs.Banco;
using MagisAuto.Application.Interfaces.Banco;

namespace MagisAuto.Application.Services;

public class BancoService : IBancoService
{
    private readonly IBancoRepository _bancoRepository;

    public BancoService(IBancoRepository bancoRepository)
    {
        _bancoRepository = bancoRepository;
    }

    public async Task<IEnumerable<BancoDto>> ObtenerTodosAsync()
    {
        return await _bancoRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<BancoDto>> BuscarAsync(string? criterio)
    {
        return await _bancoRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearBancoDto dto)
    {
        return await _bancoRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int bancoId,
        ActualizarBancoDto dto)
    {
        return await _bancoRepository.ActualizarAsync(
            bancoId,
            dto);
    }

    public async Task<bool> EliminarAsync(int bancoId)
    {
        return await _bancoRepository.EliminarAsync(bancoId);
    }
}