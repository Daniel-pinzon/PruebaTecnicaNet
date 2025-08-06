using Microsoft.EntityFrameworkCore;
using TravelRequest.Domain.Entities;
using TravelRequest.Domain.Interfaces;
using TravelRequest.Infrastructure.Persistence;

namespace TravelRequest.Infrastructure.Repositories
{
    public class SolicitudViajeRepository : ISolicitudViajeRepository
    {
        private readonly ApplicationDbContext _context;

        public SolicitudViajeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SolicitudViaje>> ObtenerTodasAsync()
        {
            return await _context.SolicitudesViaje
                .Include(s => s.Usuario)
                .ToListAsync();
        }

        public async Task<SolicitudViaje?> ObtenerPorIdAsync(int id)
        {
            return await _context.SolicitudesViaje
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task CrearAsync(SolicitudViaje solicitud)
        {
            _context.SolicitudesViaje.Add(solicitud);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(SolicitudViaje solicitud)
        {
            _context.SolicitudesViaje.Update(solicitud);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var solicitud = await _context.SolicitudesViaje.FindAsync(id);
            if (solicitud != null)
            {
                _context.SolicitudesViaje.Remove(solicitud);
                await _context.SaveChangesAsync();
            }
        }
    }
}
