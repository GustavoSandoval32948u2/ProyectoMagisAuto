using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class TotalPagadoPorClienteDto
{
    public int ClienteId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public decimal? TotalPagado { get; set; }
}