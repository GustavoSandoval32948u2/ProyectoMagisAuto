using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class CuotasPorEstadoDto
{
    public int EstadoCuotaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int? Cantidad { get; set; }
    public decimal? TotalCuotas { get; set; }
}