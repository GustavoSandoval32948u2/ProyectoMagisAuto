using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Parentesco;
using MagisAuto.Application.Interfaces.Parentesco;

namespace MagisAuto.Application.Services
{
    public class ParentescoService : IParentescoService
    {
        private readonly IParentescoRepository _parentescoRepository;

        public ParentescoService(IParentescoRepository parentescoRepository)
        {
            _parentescoRepository = parentescoRepository;
        }

        public async Task<IEnumerable<ParentescoDto>> ObtenerTodosAsync()
        {
            return await _parentescoRepository.ObtenerTodosAsync();
        }

        public async Task<IEnumerable<ParentescoDto>> BuscarAsync(string? criterio)
        {
            return await _parentescoRepository.BuscarAsync(criterio);
        }

        public async Task<int> CrearAsync(CrearParentescoDto dto)
        {
            return await _parentescoRepository.CrearAsync(dto);
        }

        public async Task<bool> ActualizarAsync(
            int parentescoId,
            ActualizarParentescoDto dto)
        {
            return await _parentescoRepository.ActualizarAsync(parentescoId, dto);
        }

        public async Task<bool> EliminarAsync(int parentescoId)
        {
            return await _parentescoRepository.EliminarAsync(parentescoId);
        }
    }
}