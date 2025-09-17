using GamePicker.Core.Models;
using GamePicker.Data.Entities;

namespace GamePicker.Core.Services
{
    public interface IRecommendationService
    {
        Task<List<Recommendation>> GetRecommendationsAsync(RecommendationRequest request);
        Task<List<Recommendation>> GetAllRecommendationsAsync();
    }
}