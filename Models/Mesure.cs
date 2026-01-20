namespace GestionBornesCollecte.Api.Models
{
    public class Mesure
    {
        public int Id { get; set; }
        public int BenneId { get; set; }
        public int NiveauRemplissage { get; set; }
        public float BatterieVolt { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
