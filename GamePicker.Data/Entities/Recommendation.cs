using System.ComponentModel.DataAnnotations.Schema;

namespace GamePicker.Data.Entities
{
    [Table("T001_RECOMENDATIONS")]
    public class Recommendation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Game_url { get; set; }
    }
}