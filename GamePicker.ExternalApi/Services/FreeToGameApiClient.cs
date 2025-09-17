using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using GamePicker.ExternalApi.Models;

namespace GamePicker.ExternalApi.Services
{
    public class FreeToGameApiClient : IFreeToGameApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://www.freetogame.com/api/";

        public FreeToGameApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //-----METODO PARA OBTER A LISTA DE JOGOS-----//
        public async Task<List<GameDto>> GetGames()
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}games");
            var games = JsonSerializer.Deserialize<List<GameDto>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return games;
        }

        //-----METODO PARA OBTER DETALHES DE UM JOGO ESPECIFICO PELO ID-----//
        public async Task<GameDetailsDto> GetGameDetails(int gameId)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}game?id={gameId}");
            var gameDetails = JsonSerializer.Deserialize<GameDetailsDto>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return gameDetails;
        }
    }
}