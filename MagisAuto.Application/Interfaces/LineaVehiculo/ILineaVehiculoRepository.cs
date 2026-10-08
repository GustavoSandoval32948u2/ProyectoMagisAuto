using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.LineaVehiculo;

namespace MagisAuto.Application.Interfaces.LineaVehiculo
{
    public interface ILineaVehiculoRepository
    {
        Task<IEnumerable<LineaVehiculoDto>> ObtenerTodosAsync();
        Task<IEnumerable<LineaVehiculoDto>> BuscarAsync(string? criterio);
        Task<int> CrearAsync(CrearLineaVehiculoDto lineaVehiculo);
        Task<bool> ActualizarAsync(int lineaVehiculoId, ActualizarLineaVehiculoDto lineaVehiculo);
        Task<bool> EliminarAsync(int lineaVehiculoId);
    }
}
