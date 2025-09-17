using System.Text.Json.Serialization;

namespace GamePicker.ExternalApi.Models
{
    public class GameDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Game_url { get; set; }

        //-----COMO O NOME DA PROPRIEDADE TEM ESPACAMENTO, USAREMOS PASCALCASE NA PROPRIEDADE E O ATRIBUTO JsonPropertyName PARA MAPEAR O NOME CORRETO DO JSON-----//
        [JsonPropertyName("minimum_system_requirements")]
        public MinimumSystemRequirementsDto MinimumSystemRequirements { get; set; }
    }

    public class MinimumSystemRequirementsDto
    {
        public string Os { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string Graphics { get; set; }
        public string Storage { get; set; }
    }
}
