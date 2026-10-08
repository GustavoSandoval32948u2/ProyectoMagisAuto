using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.LineaVehiculo
{
    public class ActualizarLineaVehiculoDto
    {
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
