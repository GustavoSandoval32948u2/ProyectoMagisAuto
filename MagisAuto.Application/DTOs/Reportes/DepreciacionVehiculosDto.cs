using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class DepreciacionVehiculosDto
{
    public int VehiculoId { get; set; }
    public string Vin { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Linea { get; set; } = string.Empty;
    public DateOnly FechaRegistro { get; set; }
    public decimal PorcentajeDepreciacion { get; set; }
    public decimal ValorAgencia { get; set; }
    public decimal? ValorDepreciacion { get; set; }
    public decimal? ValorEstimado { get; set; }
}