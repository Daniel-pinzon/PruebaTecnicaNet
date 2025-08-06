
namespace TravelRequest.Domain.Entities
{
    public class Codigos
    {
        public int Id { get; set; } 
        public string Codigo { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}

