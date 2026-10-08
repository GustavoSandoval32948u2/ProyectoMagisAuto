using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Domain.Entities
{
    public class LineaVehiculo
    {
        public int LineaVehiculoId { get; set; }
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
