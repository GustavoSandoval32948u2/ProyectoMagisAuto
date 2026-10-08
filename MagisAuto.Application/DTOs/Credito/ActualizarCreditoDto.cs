namespace MagisAuto.Application.DTOs.Credito;

public class ActualizarCreditoDto
{
    public int EstadoCreditoId { get; set; }
    public int NivelRiesgoId { get; set; }
    public decimal PorcentajeComision { get; set; }
}