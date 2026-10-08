using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Vehiculo;

namespace MagisAuto.Application.Interfaces.Vehiculo
{
    public interface IVehiculoRepository
    {
        Task<IEnumerable<VehiculoDto>> ObtenerTodosAsync();
        Task<IEnumerable<VehiculoDto>> BuscarAsync(string? criterio);
        Task<int> CrearAsync(CrearVehiculoDto dto);
        Task<bool> ActualizarAsync(
            int vehiculoId,
            ActualizarVehiculoDto dto);
        Task<bool> EliminarAsync(int vehiculoId);
    }
}
