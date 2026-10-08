using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Departamento;

namespace MagisAuto.Application.Interfaces.Departamento
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<DepartamentoDto>> ObtenerTodosAsync();
        Task<IEnumerable<DepartamentoDto>> BuscarAsync(string? criterio);
        Task<int> CrearAsync(CrearDepartamentoDto dto);
        Task<bool> ActualizarAsync(
            int departamentoId,
            ActualizarDepartamentoDto dto);
        Task<bool> EliminarAsync(int departamentoId);
    }
}
