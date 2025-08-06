
using TravelRequest.Domain.Entities;

namespace TravelRequest.Domain.Interfaces
{
    public interface ISolicitudViajeRepository
    {
        Task<IEnumerable<SolicitudViaje>> ObtenerTodasAsync();
        Task<SolicitudViaje?> ObtenerPorIdAsync(int id);
        Task CrearAsync(SolicitudViaje solicitud);
        Task ActualizarAsync(SolicitudViaje solicitud);
        Task EliminarAsync(int id);
    }
}
