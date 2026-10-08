using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Vehiculo
{
    public class CrearVehiculoDto
    {
        public int LineaVehiculoId { get; set; }
        public int ConcesionariaId { get; set; }
        public string Vin { get; set; } = string.Empty;
        public short AnioFabricacion { get; set; }
        public decimal ValorAgencia { get; set; }
        public string? Placa { get; set; }
    }
}
