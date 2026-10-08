using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Domain.Entities
{
    public class Agencia
    {
        public int AgenciaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int RegionId { get; set; }
        public string Direccion { get; set; } = string.Empty;
    }
}
