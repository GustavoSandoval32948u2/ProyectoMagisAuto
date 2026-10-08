using MagisAuto.Application.DTOs.Vendedor;
using MagisAuto.Application.Interfaces.Vendedor;

namespace MagisAuto.Application.Services;

public class VendedorService : IVendedorService
{
    private readonly IVendedorRepository _repository;

    public VendedorService(
        IVendedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<VendedorDto>> ObtenerTodosAsync()
    {
        return await _repository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<VendedorDto>> BuscarAsync(
        string? criterio)
    {
        return await _repository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(
        CrearVendedorDto dto)
    {
        return await _repository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int vendedorId,
        ActualizarVendedorDto dto)
    {
        return await _repository.ActualizarAsync(
            vendedorId,
            dto);
    }

    public async Task<bool> EliminarAsync(
        int vendedorId)
    {
        return await _repository.EliminarAsync(vendedorId);
    }
}