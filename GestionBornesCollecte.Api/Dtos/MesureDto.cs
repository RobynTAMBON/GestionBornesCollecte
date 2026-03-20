namespace GestionBornesCollecte.Api.Dtos
{
    public class MesureDto
    {
        public int NiveauRemplissage { get; set; }
        public float BatterieVolt { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
