namespace MagisAuto.Application.DTOs.Credito;

public class CrearCreditoDto
{
    public int ClienteId { get; set; }
    public int VehiculoId { get; set; }
    public int VendedorId { get; set; }
    public int NivelRiesgoId { get; set; }

    public decimal MontoEnganche { get; set; }
    public decimal PorcentajeComision { get; set; }

    public string? FiadoresIds { get; set; }

    public int? PlazoMeses { get; set; }
    public decimal? TasaInteresAnual { get; set; }
    public DateOnly? FechaAprobacion { get; set; }
}