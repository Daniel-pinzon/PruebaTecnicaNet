namespace TravelRequest.Application.DTOs
{
    public class UsuarioResponseDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Correo { get; set; }
        public required string Rol { get; set; }
    }
}
