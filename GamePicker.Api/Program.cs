using GamePicker.Core.Services;
using GamePicker.Data.Contexts;
using GamePicker.ExternalApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-----ADD DBCONTEXT NO ENTITY FMK-----//
builder.Services.AddDbContext<GamePickerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//-----ADD SERVICES-----//
builder.Services.AddHttpClient<IFreeToGameApiClient, FreeToGameApiClient>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();

var app = builder.Build();

//-----CONFIG DO HTTP REQUEST (PIPELINE)-----//
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();