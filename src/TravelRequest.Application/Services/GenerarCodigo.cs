using TravelRequest.Application.Interfaces;

namespace TravelRequest.Application.Services
{
    public class GenerarCodigoService : IGenerarCodigo
    {
        private readonly Random _random = new();

        public string GenerarCodigo()
        {
            int codigo = _random.Next(0, 1000000); 
            return codigo.ToString("D6"); 
        }
    }
}
