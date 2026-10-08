using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Cliente;

namespace MagisAuto.Application.Interfaces.Cliente
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ObtenerTodosAsync();
        Task<IEnumerable<ClienteDto>> BuscarAsync(string? criterio);
        Task<int> CrearAsync(CrearClienteDto cliente);
        Task<bool> ActualizarAsync(int clienteId, ActualizarClienteDto cliente);
        Task<bool> EliminarAsync(int clienteId);
    }
}
