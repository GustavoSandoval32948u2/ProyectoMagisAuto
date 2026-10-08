using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class VentasPorVendedorDto
{
    public int VendedorId { get; set; }
    public string CodigoEmpleado { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public int? CantidadCreditos { get; set; }
    public decimal? TotalFinanciado { get; set; }
}