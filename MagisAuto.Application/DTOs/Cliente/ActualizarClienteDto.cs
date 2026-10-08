using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Cliente
{
    public class ActualizarClienteDto
    {
        public string Nit { get; set; } = string.Empty;
        public string Dpi { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
    }
}
