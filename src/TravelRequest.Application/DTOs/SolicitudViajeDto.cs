namespace TravelRequest.Application.DTOs
{
    public class SolicitudViajeCreateDto
    {
        public string ciudad_origen { get; set; }
        public string ciudad_destino { get; set; }
        public DateTime fecha_ida { get; set; }
        public DateTime fecha_regreso { get; set; }
        public string justificacion { get; set; }
        public string estado { get; set; }
        public int UsuarioId { get; set; }
    }

    public class SolicitudViajeDto : SolicitudViajeCreateDto
    {
        public int Id { get; set; }
    }
}