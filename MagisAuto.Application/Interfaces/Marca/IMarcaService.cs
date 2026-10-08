using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Marca;

namespace MagisAuto.Application.Interfaces.Marca
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaDto>> ObtenerTodosAsync();
        Task<IEnumerable<MarcaDto>> BuscarAsync(string? criterio);
        Task<int> CrearAsync(CrearMarcaDto marca);
        Task<bool> ActualizarAsync(int marcaId, ActualizarMarcaDto marca);
        Task<bool> EliminarAsync(int marcaId);
    }
}
