using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Domain.Entities
{
    public class Region
    {
        public int RegionId { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
