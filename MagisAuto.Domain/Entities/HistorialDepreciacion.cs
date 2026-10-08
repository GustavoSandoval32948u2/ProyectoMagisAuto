namespace MagisAuto.Domain.Entities;

public class HistorialDepreciacion
{
    public int HistorialDepreciacionId { get; set; }
    public int VehiculoId { get; set; }
    public DateOnly FechaRegistro { get; set; }
    public decimal PorcentajeDepreciacion { get; set; }
}