using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Agencia
{
    public class ActualizarAgenciaDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int RegionId { get; set; }
        public int MunicipioId { get; set; }
        public string Direccion { get; set; } = string.Empty;
    }
}
