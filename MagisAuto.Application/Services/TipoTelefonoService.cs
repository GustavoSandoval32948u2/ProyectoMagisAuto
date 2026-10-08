using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.TipoTelefono;
using MagisAuto.Application.Interfaces.TipoTelefono;


namespace MagisAuto.Application.Services
{
    public class TipoTelefonoService : ITipoTelefonoService
    {
        private readonly ITipoTelefonoRepository _repository;

        public TipoTelefonoService(
            ITipoTelefonoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TipoTelefonoDto>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }

        public async Task<IEnumerable<TipoTelefonoDto>> BuscarAsync(
            string? criterio)
        {
            return await _repository.BuscarAsync(criterio);
        }

        public async Task<int> CrearAsync(
            CrearTipoTelefonoDto dto)
        {
            return await _repository.CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(
            int tipoTelefonoId,
            ActualizarTipoTelefonoDto dto)
        {
            return await _repository.ActualizarAsync(
                tipoTelefonoId,
                dto);
        }

        public async Task<bool> EliminarAsync(
            int tipoTelefonoId)
        {
            return await _repository.EliminarAsync(
                tipoTelefonoId);
        }
    }
}
