using System.ComponentModel;

namespace GestionBornesCollecte.Wpf.Models
{
    public class BenneViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Nom { get; set; }
        private string _etat;        
        public string Etat
        {
            get => _etat;
            set
            {
                _etat = value;
                OnPropertyChanged(nameof(Etat));
            }
        }

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
        public void MettreAJourEtat()
        {
            if (NiveauRemplissage >= 90)
                Etat = "PLEINE";
            else if (NiveauRemplissage >= 70)
                Etat = "ALERTE";
            else
                Etat = "OK";
        }
    }
}
