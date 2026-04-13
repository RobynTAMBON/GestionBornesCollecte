using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using GestionBornesCollecte.Wpf.DTO;
using System.Collections.ObjectModel;
using GestionBornesCollecte.Wpf.Models;
using System.Net.Http;
using System.Text.Json;

namespace GestionBornesCollecte.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection _connection;
        public ObservableCollection<BenneViewModel> Bennes { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += async (_, _) =>
            {
                await ChargerBennesAsync();
                await InitialiserSignalR();
            };
        }

        private async Task InitialiserSignalR()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5211/hubs/bennes")   
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string>("MesureRecue", json =>
            {
                var data = JsonSerializer.Deserialize<MesureRealtimeDto>(json);

                Dispatcher.Invoke(() =>
                {
                    var benne = Bennes.FirstOrDefault(b => b.Id == data.BenneId);

                    if (benne != null)
                    {
                        benne.Niveau = data.NiveauRemplissage;
                        benne.Batterie = data.BatterieVolt;
                    }
                });
            });

            try
            {
                await _connection.StartAsync();
                MesuresList.Items.Add("Connecté à SignalR");
            }
            catch (Exception ex)
            {
                MesuresList.Items.Add("Erreur : " + ex.Message);
            }
        }

        private async Task ChargerBennesAsync()
        {
            using var client = new HttpClient();

            var response = await client.GetStringAsync("https://localhost:7134/api/Bennes/overview");

            var bennes = JsonSerializer.Deserialize<List<BenneOverviewDto>>(response);

            Bennes.Clear();

            foreach (var b in bennes)
            {
                Bennes.Add(new BenneViewModel
                {
                    Id = b.Id,
                    Niveau = b.NiveauRemplissage ?? 0,
                    Batterie = b.BatterieVolt ?? 0
                });
            }
        }
    }
}