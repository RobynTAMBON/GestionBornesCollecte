namespace GestionBornesCollecte.Api.Services
{
    public class BenneEtatHelper
    {
        public static string CalculerEtat(int niveauRemplissage)
        {
            if (niveauRemplissage >= 90)
                return "PLEINE";

            if (niveauRemplissage >= 70)
                return "ALERTE";

            return "OK";
        }
    }
}
