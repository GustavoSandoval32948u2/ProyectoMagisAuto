using MagisAuto.Application.DTOs.ComisionPago;
using MagisAuto.Application.Interfaces.ComisionPago;

namespace MagisAuto.Application.Services;

public class ComisionPagoService : IComisionPagoService
{
    private readonly IComisionPagoRepository _comisionPagoRepository;

    public ComisionPagoService(
        IComisionPagoRepository comisionPagoRepository)
    {
        _comisionPagoRepository = comisionPagoRepository;
    }

    public async Task<IEnumerable<ComisionPagoDto>> ObtenerTodosAsync()
    {
        return await _comisionPagoRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<ComisionPagoDto>> BuscarAsync(
        string? criterio)
    {
        return await _comisionPagoRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearComisionPagoDto dto)
    {
        return await _comisionPagoRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int comisionPagoId,
        ActualizarComisionPagoDto dto)
    {
        return await _comisionPagoRepository.ActualizarAsync(
            comisionPagoId,
            dto);
    }

    public async Task<bool> EliminarAsync(int comisionPagoId)
    {
        return await _comisionPagoRepository.EliminarAsync(
            comisionPagoId);
    }
}