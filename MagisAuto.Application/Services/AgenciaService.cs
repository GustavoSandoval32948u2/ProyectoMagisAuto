using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Agencia;
using MagisAuto.Application.Interfaces.Agencia;

namespace MagisAuto.Application.Services
{
    public class AgenciaService : IAgenciaService
    {
        private readonly IAgenciaRepository _repository;

        public AgenciaService(IAgenciaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AgenciaDto>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }

        public async Task<IEnumerable<AgenciaDto>> BuscarAsync(
            string? criterio)
        {
            return await _repository.BuscarAsync(criterio);
        }

        public async Task<int> CrearAsync(
            CrearAgenciaDto dto)
        {
            return await _repository.CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(
            int agenciaId,
            ActualizarAgenciaDto dto)
        {
            return await _repository.ActualizarAsync(
                agenciaId,
                dto);
        }

        public async Task<bool> EliminarAsync(
            int agenciaId)
        {
            return await _repository.EliminarAsync(
                agenciaId);
        }
    }
}

