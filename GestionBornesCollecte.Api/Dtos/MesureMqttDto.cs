using System.Text.Json.Serialization;

namespace GestionBornesCollecte.Api.Dtos
{
    public class MesureMqttDto
    {

        [JsonPropertyName("benneId")]
        public int BenneId { get; set; }


        [JsonPropertyName("niveauRemplissage")]
        public int NiveauRemplissage { get; set; }


        [JsonPropertyName("batterieVolt")]
        public float BatterieVolt { get; set; }
    }
}
