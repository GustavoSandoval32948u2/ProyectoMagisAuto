using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class CreditosAprobadosPorFechaDto
{
    public string NumeroObligacion { get; set; } = string.Empty;
    public DateOnly FechaAprobacion { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Linea { get; set; } = string.Empty;
    public string Vendedor { get; set; } = string.Empty;
    public decimal MontoNetoFinanciado { get; set; }
    public string Estado { get; set; } = string.Empty;
}