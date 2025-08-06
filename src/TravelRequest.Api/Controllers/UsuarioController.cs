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
        private readonly JwtTokenGenerator _jwtGenerator; 
        private readonly GenerarCodigoService _generarCodigoService; 
        private readonly CodigoService _codigoService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(UsuarioService usuarioService, ILogger<UsuarioController> logger, JwtTokenGenerator jwtTokenGenerator,GenerarCodigoService generarCodigoService,CodigoService codigoService)
        {
            _usuarioService = usuarioService;
            _jwtGenerator = jwtTokenGenerator;
            _generarCodigoService = generarCodigoService;
            _codigoService = codigoService;
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

        [HttpPost("solicitar-codigo")]
        public async Task<IActionResult> SolicitarCodigo([FromBody] CorreoDto dto)
        {
            try
            {
                var usuario = await _usuarioService.ValidarCorreo(dto.correo);
                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                var codigoGenerado = _generarCodigoService.GenerarCodigo();
                //var num = "123";
                await _codigoService.CrearCodigo(new CodigoCreateDto
                {
                    Codigo = codigoGenerado,
                    UsuarioId = usuario.Id,
                    FechaInicio = DateTime.UtcNow,
                    FechaFin = DateTime.UtcNow.AddMinutes(5),
                    Estado = true
                });

                return Ok(new
                {
                    message = "Código solicitado correctamente",
                    codigo = codigoGenerado // una variable, por ejemplo
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al solicitar el código");
                return StatusCode(500, "Ocurrió un error al solicitar el código");
            }
        }

        [HttpPost("cambiar-password")]
        public async Task<IActionResult> CambiarPassword([FromBody] CambarPasswordDto dto)
        {
            try
            {
                var codigoValido = await _codigoService.ValidarCodigo(dto.codigo);
                if (codigoValido == null)
                {
                    return BadRequest("Código no encontrado o inactivo");
                }
                if (dto.password != dto.confirm_password)
                {
                    return BadRequest("Las contraseñas no coinciden");
                }
                await _usuarioService.CambiarPasswordAsync(codigoValido.Id, dto.password);
                await _codigoService.ActualizarCodigo(new CodigoCreateDto
                {
                    Codigo=codigoValido.Codigo,
                    Estado=false
                });
                return Ok(new { message = "Contraseña cambiada correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar la contraseña");
                return StatusCode(500, "Ocurrió un error al cambiar la contraseña");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Aprobador")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();
            return Ok(usuarios);
        }
    }
}
