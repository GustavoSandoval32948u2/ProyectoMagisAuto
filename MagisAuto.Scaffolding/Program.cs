using MagisAuto.Scaffolding.Models;

namespace MagisAuto.Scaffolding
{
    public class Program
    {
        static void Main(string[] args)
        {
            var entidad = new EntityDefinition
            {
                EntityName = "LineaVehiculo",
                IdName = "LineaVehiculoId",
                Fields =
        {
            new FieldDefinition
            {
                Name = "MarcaId",
                CSharpType = "int",
                SqlType = "Int",
                Nullable = false
            },
            new FieldDefinition
            {
                Name = "Nombre",
                CSharpType = "string",
                SqlType = "VarChar",
                Size = 100,
                Nullable = false
            }
        }
            };

            Console.WriteLine($"Entidad: {entidad.EntityName}");
            Console.WriteLine($"ID: {entidad.IdName}");

            foreach (var campo in entidad.Fields)
            {
                Console.WriteLine(
                    $"{campo.Name} → {campo.CSharpType} → {campo.SqlType}");
            }
        }
    }
}

