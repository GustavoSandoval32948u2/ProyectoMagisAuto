using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Municipio
{
    public class ActualizarMunicipioDto
    {
        public int DepartamentoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
