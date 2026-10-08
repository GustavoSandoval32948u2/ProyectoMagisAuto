namespace MagisAuto.Application.DTOs.Referencia;

public class CrearReferenciaDto
{
    public int ClienteId { get; set; }
    public int ParentescoId { get; set; }
    public string LugarTrabajo { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
}