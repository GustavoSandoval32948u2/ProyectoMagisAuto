using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class PagosPorFechaYMedioDto
{
    public int PagoId { get; set; }
    public DateTime FechaHoraPago { get; set; }
    public string NumeroObligacion { get; set; } = string.Empty;
    public string MedioPago { get; set; } = string.Empty;
    public decimal MontoPagado { get; set; }
    public string? Banco { get; set; }
    public string? Referencia { get; set; }
}