using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class CreditosPorAgenciaDto
{
    public int AgenciaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int? CantidadCreditos { get; set; }
    public decimal? TotalFinanciado { get; set; }
}