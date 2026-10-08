using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Domain.Entities
{
    public class Vehiculo
    {
        public int VehiculoId { get; set; }
        public int MarcaId { get; set; }
        public int LineaVehiculoId { get; set; }
        public string Vin { get; set; } = string.Empty;
        public string CodigoInterno { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public short AnioFabricacion { get; set; }
        public decimal ValorAgencia { get; set; }
        public string? Placa { get; set; }
    }
}
