using MagisAuto.Application.DTOs.Vendedor;

namespace MagisAuto.Application.Interfaces.Vendedor;

public interface IVendedorRepository
{
    Task<IEnumerable<VendedorDto>> ObtenerTodosAsync();

    Task<IEnumerable<VendedorDto>> BuscarAsync(
        string? criterio);

    Task<int> CrearAsync(
        CrearVendedorDto dto);

    Task<bool> ActualizarAsync(
        int vendedorId,
        ActualizarVendedorDto dto);

    Task<bool> EliminarAsync(
        int vendedorId);
}