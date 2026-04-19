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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestionBornesCollecte.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private HubConnection _connection;
        public ObservableCollection<BenneViewModel> Bennes { get; set; } = new();

        private BenneViewModel _selectedBenne;
        public BenneViewModel SelectedBenne
        {
            get => _selectedBenne;
            set
            {
                if (_selectedBenne != value)
                {
                    _selectedBenne = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HasSelectedBenne));
                    updateLayoutColonnes();
                }
            }
        }
        public bool HasSelectedBenne => SelectedBenne != null;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


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
                        benne.NiveauRemplissage = data.NiveauRemplissage;
                        benne.BatterieVolt = data.BatterieVolt;
                        benne.Etat = data.Etat;
                    }
                });
            });

            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de connexion SignalR : {ex.Message}");
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
                    Nom = b.Nom,
                    NiveauRemplissage = b.NiveauRemplissage ?? 0,
                    BatterieVolt = b.BatterieVolt ?? 0,
                    Etat = b.Etat
                });
            }
        }

        private void Benne_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is BenneViewModel benne)
            {
                if (SelectedBenne == benne)
                {
                    SelectedBenne = null;
                    return;
                }
                SelectedBenne = benne;
            }
        }

        public void updateLayoutColonnes()
        {
            if (!HasSelectedBenne)
            {
                DetailColumn.Width = new GridLength(0);
            }else
            {
                DetailColumn.Width = new GridLength(2, GridUnitType.Star);
            }
        }
    }
}