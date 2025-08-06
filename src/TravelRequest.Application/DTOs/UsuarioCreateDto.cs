using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRequest.Application.DTOs
{
    public class UsuarioCreateDto
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Correo { get; set; }
        public required string Password { get; set; }
        public required string Rol { get; set; }
    }
}
