using MQTTnet;
using System.Text;

namespace GestionBornesCollecte.Api.Services
{
    public class MqttService
    {
        private IMqttClient _client;

        public async Task StartAsync()
        {
            // Création du client MQTT (v5)
            var factory = new MqttClientFactory();
            _client = factory.CreateMqttClient();

            // Configuration de la connexion
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost", 1883)
                .WithClientId(Guid.NewGuid().ToString())
                .Build();

            // Connexion au broker
            await _client.ConnectAsync(options);
            Console.WriteLine("Connecté au broker MQTT.");

            // Abonnement au topic
            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic("temp/b213/tambon")
                .Build();

            await _client.SubscribeAsync(topicFilter);
            Console.WriteLine("Abonné au topic 'temp/b213/tambon'.");

            // Réception des messages
            _client.ApplicationMessageReceivedAsync += e =>
            {
                var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Console.WriteLine($"MQTT reçu : {message}");
                return Task.CompletedTask;
            };

        }
    }


}
