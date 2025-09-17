using GamePicker.Core.Models;
using GamePicker.Data.Contexts;
using GamePicker.Data.Entities;
using GamePicker.ExternalApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GamePicker.Core.Services
{
    public class RecommendationService : IRecommendationService
    {
        //-----INJECAO DE DEPENDENCIAS-----//
        private readonly IFreeToGameApiClient _apiClient;
        private readonly GamePickerDbContext _dbContext;

        public RecommendationService(IFreeToGameApiClient apiClient, GamePickerDbContext dbContext)
        {
            _apiClient = apiClient;
            _dbContext = dbContext;
        }

        public async Task<List<Recommendation>> GetRecommendationsAsync(RecommendationRequest request)
        {
            var allGames = await _apiClient.GetGames();

            //-----FILTRO DE GENERO-----//
            var filteredGames = allGames
                .Where(game => request.Category.Contains(game.Genre, StringComparer.OrdinalIgnoreCase))
                .ToList();

            //-----FILTRO DE PLATAFORMA SELECIONADA-----//
            switch (request.Platform)
            {
                case Platform.PC:
                    filteredGames = filteredGames.Where(game => game.Platform.IndexOf("PC", StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    break;
                case Platform.Browser:
                    filteredGames = filteredGames.Where(game => game.Platform.IndexOf("Browser", StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    break;
                default:
                    //-----NO CASO DE BOTH NAO FAZ FILTRO-----//
                    break;
            }

            //-----FILTRAR POR RAM DISPONIOVEL-----//
            if (request.AvailableRam.HasValue)
            {
                var validGames = new List<GamePicker.ExternalApi.Models.GameDto>();

                foreach (var game in filteredGames)
                {
                    var details = await _apiClient.GetGameDetails(game.Id);
                    if (details?.MinimumSystemRequirements?.Memory != null)
                    {
                        var match = Regex.Match(details.MinimumSystemRequirements.Memory, @"(\d+)\s*GB");
                        if (match.Success)
                        {
                            var requiredRamMb = int.Parse(match.Groups[1].Value) * 1024;
                            if (requiredRamMb <= request.AvailableRam.Value)
                            {
                                validGames.Add(game);
                            }
                        }
                        else
                        {
                            //-----CASO O VALOR DA RAM NAO ESTEJA NO FORMATO ESPERADO, ASSUME QUE E COMPATIVEL-----//
                            validGames.Add(game);
                        }
                    }
                }

                filteredGames = validGames;
            }

            var recommendations = new List<Recommendation>();

            if (filteredGames.Any())
            {
                //-----PEGA UM TITULO ALEATORIO-----//
                var random = new Random();
                var randomGame = filteredGames[random.Next(filteredGames.Count)];

                recommendations.Add(new Recommendation
                {
                    Title = randomGame.Title,
                    Category = randomGame.Genre,
                    Game_url = randomGame.Game_url
                });
            }

            //-----SALVA RECOMENDACAO NO BANCO-----//
            await _dbContext.Recommendations.AddRangeAsync(recommendations);
            await _dbContext.SaveChangesAsync();

            return recommendations;
        }

        public async Task<List<Recommendation>> GetAllRecommendationsAsync()
        {
            return await _dbContext.Recommendations.ToListAsync();
        }
    }
}