using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class EstadoCuentaClienteDto
{
    public int CreditoId { get; set; }
    public string NumeroObligacion { get; set; } = string.Empty;
    public DateOnly FechaAprobacion { get; set; }
    public string Estado { get; set; } = string.Empty;
    public decimal MontoNetoFinanciado { get; set; }
    public decimal MontoTotalNetoRecaudable { get; set; }
    public decimal? TotalPagado { get; set; }
    public decimal? Saldo { get; set; }
}