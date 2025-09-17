using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GamePicker.Core.Models;
using GamePicker.Core.Services;
using GamePicker.Data.Contexts;
using GamePicker.Data.Entities;
using GamePicker.ExternalApi.Models;
using GamePicker.ExternalApi.Services;

namespace GamePicker.Tests
{
    public class RecommendationServiceTests
    {
        [Fact]
        public async Task GetRecommendationsAsync_Returns_Title_And_Url()
        {
            //-----SQLITE IN-MEMORY PARA SUPORTARO O MOQ-----//
            var options = new DbContextOptionsBuilder<GamePickerDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            using var dbContext = new GamePickerDbContext(options);
            dbContext.Database.OpenConnection();         // necessário para SQLite in-memory
            //-----CRIA TABELAS-----//
            dbContext.Database.EnsureCreated();

            //-----MOCK DA API EXTERNA-----//
            var apiClientMock = new Mock<IFreeToGameApiClient>();

            apiClientMock.Setup(x => x.GetGames()).ReturnsAsync(new List<GameDto>
            {
                new GameDto
                {
                    Id = 1,
                    Title = "Mocked Game", // <--- aqui está o ajuste
                    Genre = "Action",
                    Platform = "PC",
                    Game_url = "http://gameurl.com"
                }
            });

            apiClientMock.Setup(x => x.GetGameDetails(1)).ReturnsAsync(new GameDetailsDto
            {
                Id = 1,
                MinimumSystemRequirements = new MinimumSystemRequirementsDto
                {
                    Memory = "4 GB"
                }
            });

            var service = new RecommendationService(apiClientMock.Object, dbContext);

            var request = new RecommendationRequest
            {
                Category = new List<string> { "Action" },
                Platform = Platform.PC,
                AvailableRam = 8192
            };

            //-----RESULTADO-----//
            var result = await service.GetRecommendationsAsync(request);
            Assert.Single(result);
            Assert.Equal("Mocked Game", result[0].Title);
            Assert.Equal("http://gameurl.com", result[0].Game_url);
        }
    }
}
