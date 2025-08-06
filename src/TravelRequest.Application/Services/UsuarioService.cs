using TravelRequest.Domain.Entities;
using TravelRequest.Domain.Interfaces;
using TravelRequest.Application.Interfaces;
using TravelRequest.Application.DTOs;

namespace TravelRequest.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UsuarioService(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task CrearUsuarioAsync(UsuarioCreateDto dto)
        {
            var hash = _passwordHasher.HashPassword(dto.Password);

            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                correo = dto.Correo,
                password = hash,
                Rol = dto.Rol

            };

            await _usuarioRepository.CrearAsync(nuevoUsuario);
        }

        public async Task<IEnumerable<UsuarioResponseDto>> ObtenerUsuariosAsync()
        {
           var usuarios = await _usuarioRepository.ObtenerTodosAsync();
            return usuarios.Select(u => new UsuarioResponseDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Correo = u.correo,
                Rol = u.Rol
            });
        }

        public async Task<Usuario?> ValidarCredencialesAsync(string correo, string password)
        {
            var usuario = await _usuarioRepository.ObtenerPorCorreoAsync(correo);
            if (usuario == null)
                return null;

            bool valido = _passwordHasher.VerifyHashedPassword(usuario.password, password);
            return valido ? usuario : null;
        }

        public async Task<Usuario?> ValidarCorreo(string correo)
        {
            var usuario = await _usuarioRepository.ObtenerPorCorreoAsync(correo);
            if (usuario == null)
                return null;

            return usuario;
        }

        public async Task<Usuario?> CambiarPasswordAsync(int id,string password)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);
            if(usuario == null)
            {
                return null;
            }
            var hash = _passwordHasher.HashPassword(password);
            usuario.password = hash;
            await _usuarioRepository.ActualizarAsync(usuario);
            return usuario;
        }
    }
}
