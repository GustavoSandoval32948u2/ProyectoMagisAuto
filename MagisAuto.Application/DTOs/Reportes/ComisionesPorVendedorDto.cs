using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class ComisionesPorVendedorDto
{
    public int VendedorId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public int? CantidadComisiones { get; set; }
    public decimal? TotalComisiones { get; set; }
}