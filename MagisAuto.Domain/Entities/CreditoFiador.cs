namespace MagisAuto.Domain.Entities;

public class CreditoFiador
{
    public int CreditoFiadorId { get; set; }
    public int CreditoId { get; set; }
    public int PersonaId { get; set; }
}