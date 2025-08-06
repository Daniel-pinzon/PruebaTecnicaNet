using Microsoft.EntityFrameworkCore;
using TravelRequest.Domain.Entities;

namespace TravelRequest.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<SolicitudViaje> SolicitudesViaje { get; set; }
        public DbSet<Codigos> Codigos { get; set; }
    }
}
