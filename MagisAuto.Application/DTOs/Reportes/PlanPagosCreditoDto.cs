using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class PlanPagosCreditoDto
{
    public int NumeroCuota { get; set; }
    public DateOnly FechaVencimiento { get; set; }
    public decimal MontoCapital { get; set; }
    public decimal MontoInteres { get; set; }
    public decimal MontoManejoCuenta { get; set; }
    public decimal MontoSeguroVehiculo { get; set; }
    public decimal MontoSeguroVida { get; set; }
    public decimal? MontoTotal { get; set; }
    public string Estado { get; set; } = string.Empty;
    public decimal? Pagado { get; set; }
    public decimal? Saldo { get; set; }
}