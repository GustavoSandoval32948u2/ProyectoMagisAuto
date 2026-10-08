using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Cliente;
using MagisAuto.Application.Interfaces.Cliente;

namespace MagisAuto.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteDto>> ObtenerTodosAsync()
        {
            return await _clienteRepository.ObtenerTodosAsync();
        }

        public async Task<IEnumerable<ClienteDto>> BuscarAsync(string? criterio)
        {
            return await _clienteRepository.BuscarAsync(criterio);
        }

        public async Task<int> CrearAsync(CrearClienteDto cliente)
        {
            return await _clienteRepository.CrearAsync(cliente);
        }

        public async Task<bool> ActualizarAsync(
            int clienteId,
            ActualizarClienteDto cliente)
        {
            return await _clienteRepository.ActualizarAsync(
                clienteId,
                cliente);
        }

        public async Task<bool> EliminarAsync(int clienteId)
        {
            return await _clienteRepository.EliminarAsync(clienteId);
        }
    }
}
 