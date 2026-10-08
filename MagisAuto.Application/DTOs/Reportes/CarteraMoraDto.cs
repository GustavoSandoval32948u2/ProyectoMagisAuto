using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class CarteraMoraDto
{
    public string NumeroObligacion { get; set; } = string.Empty;
    public string Cliente { get; set; } = string.Empty;
    public int NumeroCuota { get; set; }
    public DateOnly FechaVencimiento { get; set; }
    public int? DiasAtraso { get; set; }
    public decimal? MontoTotal { get; set; }
    public decimal? Pagado { get; set; }
    public decimal? SaldoPendiente { get; set; }
}