using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.LineaVehiculo;
using MagisAuto.Application.Interfaces.LineaVehiculo;

namespace MagisAuto.Application.Services
{
    public class LineaVehiculoService : ILineaVehiculoService
    {
        private readonly ILineaVehiculoRepository _repository;

        public LineaVehiculoService(ILineaVehiculoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LineaVehiculoDto>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }

        public async Task<IEnumerable<LineaVehiculoDto>> BuscarAsync(string? criterio)
        {
            return await _repository.BuscarAsync(criterio);
        }

        public async Task<int> CrearAsync(CrearLineaVehiculoDto dto)
        {
            return await _repository.CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(
            int lineaVehiculoId,
            ActualizarLineaVehiculoDto dto)
        {
            return await _repository.ActualizarAsync(lineaVehiculoId, dto);
        }

        public async Task<bool> EliminarAsync(int lineaVehiculoId)
        {
            return await _repository.EliminarAsync(lineaVehiculoId);
        }
    }
}
