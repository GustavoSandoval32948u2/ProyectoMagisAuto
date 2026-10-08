namespace MagisAuto.Domain.Entities;

public class ParametroFinanciero
{
    public int ParametroFinancieroId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}