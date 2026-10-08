using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Marca;
using MagisAuto.Application.Interfaces.Marca;

namespace MagisAuto.Application.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcaService(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<IEnumerable<MarcaDto>> ObtenerTodosAsync()
        {
            return await _marcaRepository.ObtenerTodosAsync();
        }

        public async Task<IEnumerable<MarcaDto>> BuscarAsync(string? criterio)
        {
            return await _marcaRepository.BuscarAsync(criterio);
        }

        public async Task<int> CrearAsync(CrearMarcaDto marca)
        {
            return await _marcaRepository.CrearAsync(marca);
        }

        public async Task<bool> ActualizarAsync(int marcaId, ActualizarMarcaDto marca)
        {

            return await _marcaRepository.ActualizarAsync(marcaId, marca);
        }

        public async Task<bool> EliminarAsync(int marcaId)
        {
            return await _marcaRepository.EliminarAsync(marcaId);
        }
    }
}
