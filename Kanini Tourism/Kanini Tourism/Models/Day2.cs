using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kanini_Tourism.Models
{
    public class Day2
    {
        [Key]
        public int Pack_Id { get; set; }
        public string Pack_Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public int Days { get; set; }
        public int Cost_per_person { get; set; }
        public string Day1_Locations { get; set; } = string.Empty;
        public string Image1 { get; set; }
        public string Day2_Locations { get; set; } = string.Empty;
        public string Image2 { get; set; }
        public string Day1_Hotel { get; set; } = string.Empty;
        public string Hotel_Image1 { get; set; }
        public string Day2_Hotel { get; set; } = string.Empty;
        public string Hotel_Image2 { get; set; }
        public string Day1_Description { get; set; } = string.Empty;
        public string Day2_Description { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;
        public double Phone_Number { get; set; }
        public string Agency_Name { get; set; } = string.Empty;

        public User? User { get; set; }
    }
}
