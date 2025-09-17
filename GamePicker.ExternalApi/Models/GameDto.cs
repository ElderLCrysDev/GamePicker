using System.Text.Json.Serialization;

namespace GamePicker.ExternalApi.Models
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Game_url { get; set; }
        public string Platform { get; set; }
        public string Genre { get; set; }
    }
}