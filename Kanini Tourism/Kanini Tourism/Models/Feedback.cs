using System.ComponentModel.DataAnnotations;

namespace Kanini_Tourism.Models
{
    public class Feedback
    {
        [Key]
        public int FeedId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
