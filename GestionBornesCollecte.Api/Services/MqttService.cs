using GestionBornesCollecte.Api.Dtos;
using MQTTnet;
using System.Text;
using System.Text.Json;

namespace GestionBornesCollecte.Api.Services
{
    public class MqttService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private IMqttClient _client;

        // Injection de dépendance
        public MqttService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync()
        {
            var factory = new MqttClientFactory();
            _client = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost", 1883)
                .WithClientId(Guid.NewGuid().ToString())
                .Build();

            await _client.ConnectAsync(options);
            Console.WriteLine("Connecté au broker MQTT.");

            await _client.SubscribeAsync("temp/b213/tambon");
            Console.WriteLine("Abonné au topic \"temp/b213/tambon\".");

            // handler ASYNC
            _client.ApplicationMessageReceivedAsync += async e =>
            {
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Console.WriteLine($"MQTT reçu : {payload}");

                var dto = JsonSerializer.Deserialize<MesureMqttDto>(payload);
                if (dto == null) return;
                Console.WriteLine($"DTO reçu -> BenneId={dto.BenneId}, Niveau={dto.NiveauRemplissage}, Batterie={dto.BatterieVolt}");
                using var scope = _scopeFactory.CreateScope();
                var mesureService = scope.ServiceProvider.GetRequiredService<IMesureService>();

                await mesureService.TraiterMesureAsync(dto);
            };
        }
    }
}
