using GestionBornesCollecte.Api.Data;
using GestionBornesCollecte.Api.Dtos;
using GestionBornesCollecte.Api.Models;

namespace GestionBornesCollecte.Api.Services
{
    public class MesureService : IMesureService
    {
        private readonly ApplicationDbContext _context;

        public MesureService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task TraiterMesureAsync(MesureMqttDto dto)
        {
            var mesure = new Mesure
            {
                BenneId = dto.BenneId,
                NiveauRemplissage = dto.NiveauRemplissage,
                BatterieVolt = dto.BatterieVolt,
                Timestamp = DateTime.UtcNow
            };

            _context.Mesures.Add(mesure);
            await _context.SaveChangesAsync();
        }
    }
}
