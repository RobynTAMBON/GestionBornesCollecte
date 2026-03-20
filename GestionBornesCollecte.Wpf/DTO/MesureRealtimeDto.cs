using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBornesCollecte.Wpf.DTO
{
    public class MesureRealtimeDto
    {
        public int BenneId { get; set; }
        public int NiveauRemplissage { get; set; }
        public float BatterieVolt { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
