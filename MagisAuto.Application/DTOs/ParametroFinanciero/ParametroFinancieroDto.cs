namespace MagisAuto.Application.DTOs.ParametroFinanciero;

public class ParametroFinancieroDto
{
    public int ParametroFinancieroId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}