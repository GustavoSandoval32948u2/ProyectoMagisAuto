using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Municipio;

namespace MagisAuto.Application.Interfaces.Municipio
{
    public interface IMunicipioService
    {
        Task<IEnumerable<MunicipioDto>> ObtenerTodosAsync();
        Task<IEnumerable<MunicipioDto>> BuscarAsync(string? criterio);
        Task<int> CrearAsync(CrearMunicipioDto dto);
        Task<bool> ActualizarAsync(
            int municipioId,
            ActualizarMunicipioDto dto);
        Task<bool> EliminarAsync(int municipioId);
    }
}
