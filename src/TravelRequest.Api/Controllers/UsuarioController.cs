using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using TravelRequest.Application.Services;
using TravelRequest.Application.DTOs;
using TravelRequest.Infrastructure.Services;

namespace TravelRequest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly JwtTokenGenerator _jwtGenerator; // <- Aquí necesitas un generador de tokens JWT
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(UsuarioService usuarioService, ILogger<UsuarioController> logger, JwtTokenGenerator jwtTokenGenerator)
        {
            _usuarioService = usuarioService;
            _jwtGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioCreateDto dto)
        {
            try
            {
                await _usuarioService.CrearUsuarioAsync(dto);
                return Ok(new { message = "Usuario creado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario");
                return StatusCode(500, "Ocurrió un error al crear el usuario");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var usuario = await _usuarioService.ValidarCredencialesAsync(dto.correo, dto.password);
            if (usuario == null)
                return Unauthorized("Credenciales inválidas");

            var token = _jwtGenerator.GenerarToken(usuario); // <- Aquí necesitas un generador de tokens JWT
            return Ok(new { token });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();
            return Ok(usuarios);
        }
    }
}
