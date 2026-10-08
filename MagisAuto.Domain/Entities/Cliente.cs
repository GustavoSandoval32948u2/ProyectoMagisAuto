namespace MagisAuto.Domain.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}