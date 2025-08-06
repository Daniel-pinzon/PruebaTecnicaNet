using Microsoft.EntityFrameworkCore;
using TravelRequest.Domain.Entities;
using TravelRequest.Domain.Interfaces;
using TravelRequest.Infrastructure.Persistence;

namespace TravelRequest.Infrastructure.Repositories
{
    public class CodigoRepository : IcodigoRepository
    {
        private readonly ApplicationDbContext _context;

        public CodigoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Codigos>> ObtenerTodosAsync()
        {
            return await _context.Codigos.ToListAsync();
        }

        public async Task<Codigos?> ValidarCodigo(string codigo)
        {
            var ahora = DateTime.UtcNow;

            return await _context.Codigos
                .Where(c => c.Codigo == codigo && c.Estado && ahora <= c.FechaFin)
                .FirstOrDefaultAsync();
        }

        public async Task CrearAsync(Codigos codigos)
        {
            _context.Codigos.Add(codigos);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Codigos codigos)
        {
            _context.Codigos.Update(codigos);
            await _context.SaveChangesAsync();
        }

        public async Task<Codigos?> ObtenerCodigo(string codigo)
        {
            return await _context.Codigos
                .Where(c => c.Codigo == codigo)
                .FirstOrDefaultAsync();
        }
    }
}
