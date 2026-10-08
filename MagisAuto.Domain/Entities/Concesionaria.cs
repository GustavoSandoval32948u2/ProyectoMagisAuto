namespace MagisAuto.Domain.Entities;

public class Concesionaria
{
    public int ConcesionariaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? NIT { get; set; }
}