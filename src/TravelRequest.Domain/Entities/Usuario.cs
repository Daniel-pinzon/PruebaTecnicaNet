

namespace TravelRequest.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string correo { get; set; }
        public required string password { get; set; }
        public required string Rol { get; set; }

        public ICollection<SolicitudViaje> Solicitudes { get; set; } = new List<SolicitudViaje>();
    }

}
