using GestionBornesCollecte.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionBornesCollecte.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Benne> Bennes { get; set; }
        public DbSet<Mesure> Mesures { get; set; }
    }
}
