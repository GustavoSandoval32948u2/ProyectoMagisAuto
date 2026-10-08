namespace MagisAuto.Domain.Entities;

public class TelefonoPersona
{
    public int TelefonoPersonaId { get; set; }
    public int PersonaId { get; set; }
    public int TipoTelefonoId { get; set; }
    public string NumeroTelefono { get; set; } = string.Empty;
}