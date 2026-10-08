using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class MontoTotalFinanciadoDto
{
    public int? CantidadCreditos { get; set; }
    public decimal? TotalFinanciado { get; set; }
    public decimal? InteresesGenerados { get; set; }
    public decimal? TotalRecaudable { get; set; }
}