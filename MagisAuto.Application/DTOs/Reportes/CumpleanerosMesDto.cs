using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Reportes;

public class CumpleanerosMesDto
{
    public int ClienteId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public DateOnly? FechaNacimiento { get; set; }
    public string CorreoElectronico { get; set; } = string.Empty;
}