namespace GestionBornesCollecte.Api.Models
{
    public class Benne
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Localisation { get; set; } = string.Empty;
        public int CapaciteMax { get; set; }

        // relation 1 → N
        public ICollection<Mesure> Mesures { get; set; } = new List<Mesure>();
    }

}
