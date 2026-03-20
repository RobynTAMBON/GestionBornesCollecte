using GestionBornesCollecte.Api.Dtos;

namespace GestionBornesCollecte.Api.Services
{
    public interface IBenneService
    {
        Task<List<BenneOverviewDto>> GetOverviewAsync();
    }
}
