using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Municipio;
using MagisAuto.Application.Interfaces.Municipio;

namespace MagisAuto.Application.Services
{
    public class MunicipioService : IMunicipioService
    {
        private readonly IMunicipioRepository _repository;

        public MunicipioService(IMunicipioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MunicipioDto>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }

        public async Task<IEnumerable<MunicipioDto>> BuscarAsync(
            string? criterio)
        {
            return await _repository.BuscarAsync(criterio);
        }

        public async Task<int> CrearAsync(CrearMunicipioDto dto)
        {
            return await _repository.CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(
            int municipioId,
            ActualizarMunicipioDto dto)
        {
            return await _repository.ActualizarAsync(
                municipioId,
                dto);
        }

        public async Task<bool> EliminarAsync(int municipioId)
        {
            return await _repository.EliminarAsync(municipioId);
        }
    }
}
