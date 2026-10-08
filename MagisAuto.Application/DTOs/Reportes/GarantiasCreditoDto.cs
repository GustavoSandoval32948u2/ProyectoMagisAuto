using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class GarantiasCreditoDto
{
    public int GarantiaId { get; set; }
    public string TipoGarantia { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal ValorGarantia { get; set; }
}