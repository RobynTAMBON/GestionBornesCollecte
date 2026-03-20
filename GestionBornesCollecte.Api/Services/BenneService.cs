using GestionBornesCollecte.Api.Data;
using GestionBornesCollecte.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GestionBornesCollecte.Api.Services
{
    public class BenneService : IBenneService
    {
        private readonly ApplicationDbContext _context;

        public BenneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BenneOverviewDto>> GetOverviewAsync()
        {
            return await _context.Bennes
                .Select(b => new BenneOverviewDto
                {
                    Id = b.Id,
                    Nom = b.Nom,

                    // dernière mesure (s'il y en a)
                    NiveauRemplissage = b.Mesures
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => (int?)m.NiveauRemplissage)
                        .FirstOrDefault(),

                    BatterieVolt = b.Mesures
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => (float?)m.BatterieVolt)
                        .FirstOrDefault(),

                    DerniereMesure = b.Mesures
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => (DateTime?)m.Timestamp)
                        .FirstOrDefault(),

                    Etat = b.Mesures.Any()
                        ? b.Mesures
                            .OrderByDescending(m => m.Timestamp)
                            .Select(m => m.NiveauRemplissage >= 90
                                ? "PLEINE"
                                : m.NiveauRemplissage >= 70
                                    ? "ALERTE"
                                    : "OK")
                            .First()
                        : "AUCUNE_MESURE"
                })
                .ToListAsync();
        }
    }
}
