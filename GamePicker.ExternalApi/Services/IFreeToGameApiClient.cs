using GamePicker.ExternalApi.Models;

namespace GamePicker.ExternalApi.Services
{
    public interface IFreeToGameApiClient
    {
        Task<List<GameDto>> GetGames();
        Task<GameDetailsDto> GetGameDetails(int gameId);
    }
}