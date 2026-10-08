namespace MagisAuto.Application.DTOs.TelefonoReferencia;

public class TelefonoReferenciaDto
{
    public int TelefonoReferenciaId { get; set; }
    public int ReferenciaId { get; set; }
    public int TipoTelefonoId { get; set; }
    public string NumeroTelefono { get; set; } = string.Empty;
}