namespace MagisAuto.Domain.Entities;

public class Referencia
{
    public int ReferenciaId { get; set; }
    public int ClienteId { get; set; }
    public int ParentescoId { get; set; }
    public string LugarTrabajo { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
}