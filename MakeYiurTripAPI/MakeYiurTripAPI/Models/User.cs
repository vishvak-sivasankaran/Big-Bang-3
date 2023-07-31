using System.ComponentModel.DataAnnotations;

namespace MakeYourTripAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string? Role { get; set; }
        public string? Password { get; set; }
    }
}
