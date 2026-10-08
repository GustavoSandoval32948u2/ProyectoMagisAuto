using MagisAuto.Application.DTOs.TelefonoPersona;
using MagisAuto.Application.Interfaces.TelefonoPersona;

namespace MagisAuto.Application.Services;

public class TelefonoPersonaService : ITelefonoPersonaService
{
    private readonly ITelefonoPersonaRepository _telefonoPersonaRepository;

    public TelefonoPersonaService(
        ITelefonoPersonaRepository telefonoPersonaRepository)
    {
        _telefonoPersonaRepository = telefonoPersonaRepository;
    }

    public async Task<IEnumerable<TelefonoPersonaDto>> ObtenerTodosAsync()
    {
        return await _telefonoPersonaRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<TelefonoPersonaDto>> BuscarAsync(
        string? criterio)
    {
        return await _telefonoPersonaRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearTelefonoPersonaDto dto)
    {
        return await _telefonoPersonaRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int telefonoPersonaId,
        ActualizarTelefonoPersonaDto dto)
    {
        return await _telefonoPersonaRepository.ActualizarAsync(
            telefonoPersonaId,
            dto);
    }

    public async Task<bool> EliminarAsync(int telefonoPersonaId)
    {
        return await _telefonoPersonaRepository.EliminarAsync(
            telefonoPersonaId);
    }
}