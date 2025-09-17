using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GamePicker.Core.Models
{
    public class RecommendationRequest
    {
        [Required]
        public List<string> Category { get; set; }
        public Platform Platform { get; set; }
        public int? AvailableRam { get; set; }
    }
}