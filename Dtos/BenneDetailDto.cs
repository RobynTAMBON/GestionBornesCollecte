namespace GestionBornesCollecte.Api.Dtos
{
    public class BenneDetailDto
    { 
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Localisation { get; set; } = null!;
        public int CapaciteMax { get; set; }

        public DateTime? DerniereMesure { get; set; }
        public int? NiveauRemplissage { get; set; }
        public float? BatterieVolt { get; set; }

    }
}
