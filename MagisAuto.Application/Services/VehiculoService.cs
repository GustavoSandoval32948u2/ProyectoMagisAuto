using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Vehiculo;
using MagisAuto.Application.Interfaces.Vehiculo;

namespace MagisAuto.Application.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _repository;

        public VehiculoService(IVehiculoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<VehiculoDto>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }

        public async Task<IEnumerable<VehiculoDto>> BuscarAsync(string? criterio)
        {
            return await _repository.BuscarAsync(criterio);
        }

        public async Task<int> CrearAsync(CrearVehiculoDto dto)
        {
            return await _repository.CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(
            int vehiculoId,
            ActualizarVehiculoDto dto)
        {
            return await _repository.ActualizarAsync(vehiculoId, dto);
        }

        public async Task<bool> EliminarAsync(int vehiculoId)
        {
            return await _repository.EliminarAsync(vehiculoId);
        }
    }
}
