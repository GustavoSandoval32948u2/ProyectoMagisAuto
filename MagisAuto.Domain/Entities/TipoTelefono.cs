using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Domain.Entities
{
    public class TipoTelefono
    {
        public int TipoTelefonoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
