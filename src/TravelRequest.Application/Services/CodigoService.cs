using TravelRequest.Domain.Entities;
using TravelRequest.Domain.Interfaces;
using TravelRequest.Application.Interfaces;
using TravelRequest.Application.DTOs;

namespace TravelRequest.Application.Services
{
    public class CodigoService
    {
        private readonly IcodigoRepository _codigoRepository;

        public CodigoService(IcodigoRepository codigoRepository)
        {
            _codigoRepository = codigoRepository;
        }

        public async Task CrearCodigo(CodigoCreateDto dto)
        {
            var nuevoCodigo = new Codigos
            {
                Codigo = dto.Codigo,
                UsuarioId = dto.UsuarioId,
                FechaInicio = dto.FechaInicio.Value,
                FechaFin = dto.FechaFin.Value,
                Estado = dto.Estado.Value
            };
            await _codigoRepository.CrearAsync(nuevoCodigo);
        }

        public async Task ActualizarCodigo(CodigoCreateDto dto)
        {
            var codigoExistente = await _codigoRepository.ObtenerCodigo(dto.Codigo);
            if (codigoExistente == null)
                throw new Exception("Código no encontrado");

            if (dto.FechaInicio != null)
                codigoExistente.FechaInicio = dto.FechaInicio.Value;

            if (dto.FechaFin != null)
                codigoExistente.FechaFin = dto.FechaFin.Value;

            if (dto.Estado != null)
                codigoExistente.Estado = dto.Estado.Value;


            await _codigoRepository.ActualizarAsync(codigoExistente);
        }

        public async Task<Codigos?> ObtenerCodigo(string codigo)
        {
            var codigoExistente = await _codigoRepository.ObtenerCodigo(codigo);
            if (codigoExistente == null)
            {
                return null;
            }
            return codigoExistente;
        }

        public async Task<Codigos?> ValidarCodigo(string codigo)
        {
            var codigoExistente = await _codigoRepository.ValidarCodigo(codigo);
            if (codigoExistente == null)
            {
                return null;
            }
            return codigoExistente;
        }



    }
}
