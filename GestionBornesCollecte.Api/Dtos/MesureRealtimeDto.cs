namespace GestionBornesCollecte.Api.Dtos
{
    public class MesureRealtimeDto
    {
        public int BenneId { get; set; }
        public int NiveauRemplissage { get; set; }
        public float BatterieVolt { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
