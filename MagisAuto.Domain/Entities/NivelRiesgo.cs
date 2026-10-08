namespace MagisAuto.Domain.Entities;

public class NivelRiesgo
{
    public int NivelRiesgoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool RequiereFiador { get; set; }
}