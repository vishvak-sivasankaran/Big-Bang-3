using System.ComponentModel.DataAnnotations;

namespace Kanini_Tourism.Models
{
    public class Dummy
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string? Role { get; set; }
        public double Phone_Number { get; set; }
        public string? Agency_Name { get; set; }
    }
}
