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

namespace GestionBornesCollecte.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection _connection;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
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
                    MesuresList.Items.Add(
                        $"Benne {data.BenneId} - {data.NiveauRemplissage}% - {data.BatterieVolt}V"
                    );
                });
            }); ;

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
    }
}