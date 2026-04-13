using System.ComponentModel;

namespace GestionBornesCollecte.Wpf.Models
{
    public class BenneViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int _niveau;
        public int Niveau
        {
            get => _niveau;
            set
            {
                _niveau = value;
                OnPropertyChanged(nameof(Niveau));
            }
        }
        private float _batterie;
        public float Batterie
        {
            get => _batterie;
            set
            {
                _batterie = value;
                OnPropertyChanged(nameof(Batterie));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
