using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestionBornesCollecte.Wpf.DTO
{
    internal class BenneOverviewDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("nom")]
        public string Nom { get; set; }


        [JsonPropertyName("niveauRemplissage")]
        public int? NiveauRemplissage { get; set; }

        [JsonPropertyName("batterieVolt")]
        public float? BatterieVolt { get; set; }


        [JsonPropertyName("etat")]
        public string Etat { get; set; }
    }
}
