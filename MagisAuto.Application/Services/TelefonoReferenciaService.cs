using MagisAuto.Application.DTOs.TelefonoReferencia;
using MagisAuto.Application.Interfaces.TelefonoReferencia;

namespace MagisAuto.Application.Services;

public class TelefonoReferenciaService : ITelefonoReferenciaService
{
    private readonly ITelefonoReferenciaRepository _telefonoReferenciaRepository;

    public TelefonoReferenciaService(
        ITelefonoReferenciaRepository telefonoReferenciaRepository)
    {
        _telefonoReferenciaRepository = telefonoReferenciaRepository;
    }

    public async Task<IEnumerable<TelefonoReferenciaDto>> ObtenerTodosAsync()
    {
        return await _telefonoReferenciaRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<TelefonoReferenciaDto>> BuscarAsync(
        string? criterio)
    {
        return await _telefonoReferenciaRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearTelefonoReferenciaDto dto)
    {
        return await _telefonoReferenciaRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int telefonoReferenciaId,
        ActualizarTelefonoReferenciaDto dto)
    {
        return await _telefonoReferenciaRepository.ActualizarAsync(
            telefonoReferenciaId,
            dto);
    }

    public async Task<bool> EliminarAsync(int telefonoReferenciaId)
    {
        return await _telefonoReferenciaRepository.EliminarAsync(
            telefonoReferenciaId);
    }
}