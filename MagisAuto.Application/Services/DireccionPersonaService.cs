using MagisAuto.Application.DTOs.DireccionPersona;
using MagisAuto.Application.Interfaces.DireccionPersona;

namespace MagisAuto.Application.Services;

public class DireccionPersonaService : IDireccionPersonaService
{
    private readonly IDireccionPersonaRepository _direccionPersonaRepository;

    public DireccionPersonaService(
        IDireccionPersonaRepository direccionPersonaRepository)
    {
        _direccionPersonaRepository = direccionPersonaRepository;
    }

    public async Task<IEnumerable<DireccionPersonaDto>> ObtenerTodosAsync()
    {
        return await _direccionPersonaRepository.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<DireccionPersonaDto>> BuscarAsync(
        string? criterio)
    {
        return await _direccionPersonaRepository.BuscarAsync(criterio);
    }

    public async Task<int> CrearAsync(CrearDireccionPersonaDto dto)
    {
        return await _direccionPersonaRepository.CrearAsync(dto);
    }

    public async Task<bool> ActualizarAsync(
        int direccionPersonaId,
        ActualizarDireccionPersonaDto dto)
    {
        return await _direccionPersonaRepository.ActualizarAsync(
            direccionPersonaId,
            dto);
    }

    public async Task<bool> EliminarAsync(int direccionPersonaId)
    {
        return await _direccionPersonaRepository.EliminarAsync(
            direccionPersonaId);
    }
}