using GamePicker.Core.Dtos;
using GamePicker.Core.Models;
using GamePicker.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamePicker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecommendationsController : ControllerBase
    {
        //-----INJECAO DE DEPENDENCIA-----//
        private readonly IRecommendationService _recommendationService;

        public RecommendationsController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecommendations([FromQuery] RecommendationRequest request)
        {
            //-----OBTEM JOGO RECOMENDADO-----//
            var recommendations = await _recommendationService.GetRecommendationsAsync(request);

            //-----VALIDA RESULTADO-----//
            if (recommendations == null || !recommendations.Any())
            {
                return NotFound("Não foi possível encontrar uma recomendação com os filtros fornecidos.");
            }

            //-----RETORNA O TITULO E URL CASO HAJA GAME-----//
            var responseDtos = recommendations.Select(r => new RecommendationResponseDto
            {
                Title = r.Title,
                Game_url = r.Game_url
            }).ToList();

            return Ok(responseDtos);
        }

        [HttpGet("GetPreviousRecommendations")]
        public async Task<IActionResult> GetAllRecommendations()
        {
            var recommendations = await _recommendationService.GetAllRecommendationsAsync();

            if (recommendations == null || !recommendations.Any())
            {
                return NotFound("Nenhuma recomendação encontrada no banco de dados.");
            }

            var responseDtos = recommendations.Select(r => new AllRecommendationsResponseDto
            {
                Title = r.Title,
                Category = r.Category
            }).ToList();

            return Ok(responseDtos);
        }
    }
}