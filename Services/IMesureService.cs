using GestionBornesCollecte.Api.Dtos;

namespace GestionBornesCollecte.Api.Services
{
    public interface IMesureService
    {
        Task TraiterMesureAsync(MesureMqttDto dto);
    }
}
