namespace GestionBornesCollecte.Api.Dtos
{
    public class MesureMqttDto
    {
        public int BenneId { get; set; }
        public int NiveauRemplissage { get; set; }
        public float BatterieVolt { get; set; }
    }
}
