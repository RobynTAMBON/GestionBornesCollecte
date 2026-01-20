namespace GestionBornesCollecte.Api.Models
{
    public class Benne
    {
        public int Id { get; set; }

        // Eviter que le nom soit null
        public string Nom { get; set; } = string.Empty;

        // Eviter que la localisation soit null
        public string Localisation { get; set; } = string.Empty;
        public int CapaciteMax { get; set; }
    }
}
