using System.ComponentModel;

namespace GestionBornesCollecte.Wpf.Models
{
    public class BenneViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Nom { get; set; }
        public string Etat { get; set; }

        private int _niveauRemplissage;
        public int NiveauRemplissage
        {
            get => _niveauRemplissage;
            set
            {
                _niveauRemplissage = value;
                OnPropertyChanged(nameof(NiveauRemplissage));
            }
        }

        private float _batterieVolt;
        public float BatterieVolt
        {
            get => _batterieVolt;
            set
            {
                _batterieVolt = value;
                OnPropertyChanged(nameof(BatterieVolt));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
