using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Departamento;
using MagisAuto.Application.Interfaces.Departamento;

namespace MagisAuto.Application.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _repository;

        public DepartamentoService(IDepartamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DepartamentoDto>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }

        public async Task<IEnumerable<DepartamentoDto>> BuscarAsync(
            string? criterio)
        {
            return await _repository.BuscarAsync(criterio);
        }

        public async Task<int> CrearAsync(CrearDepartamentoDto dto)
        {
            return await _repository.CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(
            int departamentoId,
            ActualizarDepartamentoDto dto)
        {
            return await _repository.ActualizarAsync(
                departamentoId,
                dto);
        }

        public async Task<bool> EliminarAsync(int departamentoId)
        {
            return await _repository.EliminarAsync(departamentoId);
        }
    }
}
