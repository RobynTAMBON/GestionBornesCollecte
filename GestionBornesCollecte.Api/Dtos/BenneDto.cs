namespace GestionBornesCollecte.Api.Dtos
{
    public class BenneDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Localisation { get; set; } = null!;
        public int CapaciteMax { get; set; }
    }
}
