using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Application.DTOs.Marca
{
    public class MarcaDto
    {
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string CodigoInterno { get; set; } = string.Empty;
    }
}
