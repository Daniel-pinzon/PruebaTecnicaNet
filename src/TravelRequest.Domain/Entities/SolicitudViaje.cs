

namespace TravelRequest.Domain.Entities
{
    public class SolicitudViaje
    {
        public int Id { get; set; }
        public required string ciudad_origen { get; set; }
        public required string ciudad_destino { get; set; }
        public DateTime fecha_ida { get; set; }
        public DateTime fecha_regreso { get; set; }
        public required string justificacion { get; set; }
        public required string estado { get; set; } 
    }
}
