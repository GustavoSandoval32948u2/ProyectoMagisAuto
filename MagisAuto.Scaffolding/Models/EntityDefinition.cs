using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Scaffolding.Models
{
    public class EntityDefinition
    {
        public string EntityName { get; set; } = string.Empty;

        public string IdName { get; set; } = string.Empty;

        public List<FieldDefinition> Fields { get; set; } = new();
    }
}
