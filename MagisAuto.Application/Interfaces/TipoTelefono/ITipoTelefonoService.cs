using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.TipoTelefono;

namespace MagisAuto.Application.Interfaces.TipoTelefono
{
    public interface ITipoTelefonoService
    {
        Task<IEnumerable<TipoTelefonoDto>> ObtenerTodosAsync();
        Task<IEnumerable<TipoTelefonoDto>> BuscarAsync(string? criterio);
        Task<int> CrearAsync(CrearTipoTelefonoDto dto);
        Task<bool> ActualizarAsync(
            int tipoTelefonoId,
            ActualizarTipoTelefonoDto dto);
        Task<bool> EliminarAsync(int tipoTelefonoId);
    }
}
