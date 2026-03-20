using GestionBornesCollecte.Api.Data;
using GestionBornesCollecte.Api.Dtos;
using GestionBornesCollecte.Api.Models;
using Microsoft.AspNetCore.SignalR;
using GestionBornesCollecte.Api.Hubs;
using System.Text.Json;


namespace GestionBornesCollecte.Api.Services
{
    public class MesureService : IMesureService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<BennesHub> _hub;

        public MesureService(ApplicationDbContext context, IHubContext<BennesHub> hub)
        {
            _context = context;
            _hub = hub;
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

            // WebSocket
            var MRTdto = new MesureRealtimeDto
            {
                BenneId = mesure.BenneId,
                NiveauRemplissage = mesure.NiveauRemplissage,
                BatterieVolt = mesure.BatterieVolt,
                Timestamp = mesure.Timestamp
            };
            Console.WriteLine("Envoi SignalR...");
            await _hub.Clients.All.SendAsync("MesureRecue", JsonSerializer.Serialize(MRTdto));
            Console.WriteLine("SignalR - Mesure envoyée");
        }
    }
}
