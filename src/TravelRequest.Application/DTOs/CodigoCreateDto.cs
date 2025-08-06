

namespace TravelRequest.Application.DTOs
{
    public class CodigoCreateDto
    {
        public string Codigo { get; set; } = null!;
        public int UsuarioId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? Estado { get; set; }
    }
}
