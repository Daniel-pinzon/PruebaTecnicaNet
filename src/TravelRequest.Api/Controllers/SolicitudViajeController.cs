using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelRequest.Application.Services;
using TravelRequest.Application.DTOs;

namespace TravelRequest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudViajeController : ControllerBase
    {
        private readonly SolicitudViajeService _service;

        public SolicitudViajeController(SolicitudViajeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _service.ObtenerPorUsuarioAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] SolicitudViajeCreateDto dto)
        {
            // Validación: Fecha regreso > Fecha ida
            if (dto.fecha_regreso <= dto.fecha_ida)
            {
                return BadRequest("La fecha de regreso debe ser mayor que la fecha de ida.");
            }

            // Validación: Origen ? Destino
            if (dto.ciudad_origen.Trim().ToLower() == dto.ciudad_destino.Trim().ToLower())
            {
                return BadRequest("La ciudad de origen debe ser diferente a la ciudad de destino.");
            }

            // Asocia la solicitud al usuario autenticado
            dto.UsuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _service.CrearAsync(dto);
            return Ok(new { message = "Solicitud creada correctamente" });
        }

        [HttpPut("{id}/estado")]
        [Authorize(Roles = "Aprobador")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] string nuevoEstado)
        {
            await _service.CambiarEstadoAsync(id, nuevoEstado);
            return Ok(new { message = "Estado actualizado correctamente" });
        }
    }
}