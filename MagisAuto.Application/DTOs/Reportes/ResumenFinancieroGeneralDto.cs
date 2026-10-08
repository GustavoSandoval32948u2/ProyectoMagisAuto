using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class ResumenFinancieroGeneralDto
{
    public int? TotalClientes { get; set; }
    public int? TotalCreditos { get; set; }
    public decimal? TotalFinanciado { get; set; }
    public decimal? TotalRecaudado { get; set; }
    public decimal? TotalComisiones { get; set; }
    public int? TotalVehiculos { get; set; }
}