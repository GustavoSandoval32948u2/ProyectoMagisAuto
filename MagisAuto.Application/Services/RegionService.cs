using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Region;
using MagisAuto.Application.Interfaces.Region;

namespace MagisAuto.Application.Services
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _repository;

        public RegionService(IRegionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RegionDto>> ObtenerTodosAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }


        public async Task<IEnumerable<RegionDto>> BuscarAsync(string? criterio)
        {
            return await _repository.BuscarAsync(criterio);
        }

        public async Task<int> CrearAsync(CrearRegionDto dto)
        {
            return await _repository.CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(
            int regionId,
            ActualizarRegionDto dto)
        {
            return await _repository.ActualizarAsync(regionId, dto);
        }

        public async Task<bool> EliminarAsync(int regionId)
        {
            return await _repository.EliminarAsync(regionId);
        }
    }
}
