namespace GestionBornesCollecte.Api.Dtos
{
    public class MesureRealtimeDto
    {
        public int BenneId { get; set; }
        public int NiveauRemplissage { get; set; }
        public float BatterieVolt { get; set; }
        public string Etat { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
