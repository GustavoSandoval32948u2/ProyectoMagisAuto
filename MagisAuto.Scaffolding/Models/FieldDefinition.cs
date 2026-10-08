using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagisAuto.Scaffolding.Models
{
    public class FieldDefinition
    {
        public string Name { get; set; } = string.Empty;

        public string CSharpType { get; set; } = string.Empty;

        public string SqlType { get; set; } = string.Empty;

        public int? Size { get; set; }

        public bool Nullable { get; set; }
    }
}
