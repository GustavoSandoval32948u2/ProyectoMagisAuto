using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class IngresosPorMesDto
{
    public int Mes { get; set; }
    public int? CantidadPagos { get; set; }
    public decimal? TotalIngresos { get; set; }
}