using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagisAuto.Application.DTOs.Parentesco;

namespace MagisAuto.Application.Interfaces.Parentesco
{
    public interface IParentescoRepository
    {
        Task<IEnumerable<ParentescoDto>> ObtenerTodosAsync();
        Task<IEnumerable<ParentescoDto>> BuscarAsync(string? criterio);
        Task<int> CrearAsync(CrearParentescoDto dto);
        Task<bool> ActualizarAsync(int parentescoId, ActualizarParentescoDto dto);
        Task<bool> EliminarAsync(int parentescoId);
    }
}
