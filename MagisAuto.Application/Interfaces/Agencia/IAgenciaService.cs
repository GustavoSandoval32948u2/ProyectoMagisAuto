using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Agencia;

namespace MagisAuto.Application.Interfaces.Agencia
{
    public interface IAgenciaService
    {
        Task<IEnumerable<AgenciaDto>> ObtenerTodosAsync();
        Task<IEnumerable<AgenciaDto>> BuscarAsync(string? criterio);
        Task<int> CrearAsync(CrearAgenciaDto agencia);
        Task<bool> ActualizarAsync(int agenciaId, ActualizarAgenciaDto agencia);
        Task<bool> EliminarAsync(int agenciaId);
    }
}
