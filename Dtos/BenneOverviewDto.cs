namespace GestionBornesCollecte.Api.Dtos
{
    public class BenneOverviewDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        public int? NiveauRemplissage { get; set; }
        public float? BatterieVolt { get; set; }
        public DateTime? DerniereMesure { get; set; }

        public string Etat { get; set; } = "INCONNU";
    }
}
