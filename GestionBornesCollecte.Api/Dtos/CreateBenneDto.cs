using System.ComponentModel.DataAnnotations;

namespace GestionBornesCollecte.Api.Dtos
{
    public class CreateBenneDto
    {
        [Required]
        [MaxLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Localisation { get; set; } = string.Empty;

        [Range(1, 10000)]
        public int CapaciteMax { get; set; }
    }
}
