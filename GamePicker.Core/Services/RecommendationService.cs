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
            var filteredGames = (await _apiClient.GetGames())
                //-----FILTRO DE GENERO + PLATAFORMA-----//
                .Where(game =>
                    request.Category.Contains(game.Genre, StringComparer.OrdinalIgnoreCase) &&
                    (request.Platform == Platform.Both ||
                     (request.Platform == Platform.PC && game.Platform?.IndexOf("PC", StringComparison.OrdinalIgnoreCase) >= 0) ||
                     (request.Platform == Platform.Browser && game.Platform?.IndexOf("Browser", StringComparison.OrdinalIgnoreCase) >= 0)))
                .ToList();

            //-----FILTRAR POR RAM DISPONIVEL-----//
            if (request.AvailableRam.HasValue)
            {
                var detailedGames = await Task.WhenAll(filteredGames.Select(async game =>
                {
                    var details = await _apiClient.GetGameDetails(game.Id);
                    return new { Game = game, Details = details };
                }));

                filteredGames = detailedGames
                    .Where(item =>
                    {
                        var mem = item.Details?.MinimumSystemRequirements?.Memory;
                        if (mem == null) return false;

                        var match = Regex.Match(mem, @"(\d+)\s*GB");
                        if (!match.Success) return true; //-----FORMATO DESCONHECIDO, ASSUME COMPATIVEL-----//

                        var requiredRamMb = int.Parse(match.Groups[1].Value) * 1024;
                        return requiredRamMb <= request.AvailableRam.Value;
                    })
                    .Select(item => item.Game)
                    .ToList();
            }

            var recommendations = new List<Recommendation>();

            if (filteredGames.Any())
            {
                //-----PEGA UM TITULO ALEATORIO-----//
                var random = new Random();
                var game = filteredGames[random.Next(filteredGames.Count)];

                recommendations.Add(new Recommendation
                {
                    Title = game.Title,
                    Category = game.Genre,
                    Game_url = game.Game_url
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