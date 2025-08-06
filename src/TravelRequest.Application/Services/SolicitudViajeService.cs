using TravelRequest.Domain.Entities;
using TravelRequest.Domain.Interfaces;
using TravelRequest.Application.DTOs;

namespace TravelRequest.Application.Services
{
    public class SolicitudViajeService
    {
        private readonly ISolicitudViajeRepository _repository;

        public SolicitudViajeService(ISolicitudViajeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SolicitudViajeDto>> ObtenerTodasAsync()
        {
            var solicitudes = await _repository.ObtenerTodasAsync();
            return solicitudes.Select(s => new SolicitudViajeDto
            {
                Id = s.Id,
                ciudad_origen = s.ciudad_origen,
                ciudad_destino = s.ciudad_destino,
                fecha_ida = s.fecha_ida,
                fecha_regreso = s.fecha_regreso,
                justificacion = s.justificacion,
                estado = s.estado,
                UsuarioId = s.UsuarioId
            });
        }

        public async Task<SolicitudViajeDto?> ObtenerPorIdAsync(int id)
        {
            var s = await _repository.ObtenerPorIdAsync(id);
            if (s == null) return null;
            return new SolicitudViajeDto
            {
                Id = s.Id,
                ciudad_origen = s.ciudad_origen,
                ciudad_destino = s.ciudad_destino,
                fecha_ida = s.fecha_ida,
                fecha_regreso = s.fecha_regreso,
                justificacion = s.justificacion,
                estado = s.estado,
                UsuarioId = s.UsuarioId
            };
        }

        public async Task CrearAsync(SolicitudViajeCreateDto dto)
        {
            var solicitud = new SolicitudViaje
            {
                ciudad_origen = dto.ciudad_origen,
                ciudad_destino = dto.ciudad_destino,
                fecha_ida = dto.fecha_ida,
                fecha_regreso = dto.fecha_regreso,
                justificacion = dto.justificacion,
                estado = dto.estado,
                UsuarioId = dto.UsuarioId
            };
            await _repository.CrearAsync(solicitud);
        }

        public async Task ActualizarAsync(int id, SolicitudViajeCreateDto dto)
        {
            var solicitud = await _repository.ObtenerPorIdAsync(id);
            if (solicitud == null) throw new Exception("Solicitud no encontrada");

            solicitud.ciudad_origen = dto.ciudad_origen;
            solicitud.ciudad_destino = dto.ciudad_destino;
            solicitud.fecha_ida = dto.fecha_ida;
            solicitud.fecha_regreso = dto.fecha_regreso;
            solicitud.justificacion = dto.justificacion;
            solicitud.estado = dto.estado;
            solicitud.UsuarioId = dto.UsuarioId;

            await _repository.ActualizarAsync(solicitud);
        }

        public async Task EliminarAsync(int id)
        {
            await _repository.EliminarAsync(id);
        }

        public async Task<IEnumerable<SolicitudViajeDto>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var solicitudes = await _repository.ObtenerTodasAsync();
            return solicitudes
                .Where(s => s.UsuarioId == usuarioId)
                .Select(s => new SolicitudViajeDto
                {
                    Id = s.Id,
                    ciudad_origen = s.ciudad_origen,
                    ciudad_destino = s.ciudad_destino,
                    fecha_ida = s.fecha_ida,
                    fecha_regreso = s.fecha_regreso,
                    justificacion = s.justificacion,
                    estado = s.estado,
                    UsuarioId = s.UsuarioId
                });
        }

        public async Task CambiarEstadoAsync(int id, string nuevoEstado)
        {
            var solicitud = await _repository.ObtenerPorIdAsync(id);
            if (solicitud == null) throw new Exception("Solicitud no encontrada");
            solicitud.estado = nuevoEstado;
            await _repository.ActualizarAsync(solicitud);
        }
    }
}