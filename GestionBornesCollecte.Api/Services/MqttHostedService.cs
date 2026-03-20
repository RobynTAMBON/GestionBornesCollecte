namespace GestionBornesCollecte.Api.Services
{
    public class MqttHostedService : IHostedService
    {
        private readonly MqttService _mqttService;

        public MqttHostedService(MqttService mqttService)
        {
            _mqttService = mqttService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Démarrage du service MQTT...");
            await _mqttService.StartAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Arrêt du service MQTT...");
            return Task.CompletedTask;
        }
    }
}
