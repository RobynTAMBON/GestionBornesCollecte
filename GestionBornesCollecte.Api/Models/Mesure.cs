namespace GestionBornesCollecte.Api.Models
{
    public class Mesure
    {
        public int Id { get; set; }

        // Foreign Key
        public int BenneId { get; set; }

        // Navigation vers Benne
        public Benne Benne { get; set; } = null!;

        public int NiveauRemplissage { get; set; }
        public float BatterieVolt { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
