using TravelRequest.Domain.Entities;

namespace TravelRequest.Domain.Interfaces
{
    public interface IcodigoRepository
    {
        Task<IEnumerable<Codigos>> ObtenerTodosAsync();
        Task<Codigos> ValidarCodigo(string codigo);
        Task<Codigos> ObtenerCodigo(string codigo);
        Task CrearAsync(Codigos codigos);
        Task ActualizarAsync(Codigos codigos);

    }
}
