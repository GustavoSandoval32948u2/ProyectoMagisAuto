using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class VehiculosPorMarcaDto
{
    public int MarcaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int? CantidadVehiculos { get; set; }
    public decimal? ValorAgenciaTotal { get; set; }
}