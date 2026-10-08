using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Region;

namespace MagisAuto.Application.Interfaces.Region
{
    public interface IRegionService
    {
        Task<IEnumerable<RegionDto>> ObtenerTodosAsync();
        Task<IEnumerable<RegionDto>> BuscarAsync(string? criterio);
        Task<int> CrearAsync(CrearRegionDto dto);
        Task<bool> ActualizarAsync(
            int regionId,
            ActualizarRegionDto dto);
        Task<bool> EliminarAsync(int regionId);
    }
}
