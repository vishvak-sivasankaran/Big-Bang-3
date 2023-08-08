using System.ComponentModel.DataAnnotations;

namespace Kanini_Tourism.Models
{
    public class Hotels
    {
        [Key]

        public int HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string HotelImage { get; set; } = string.Empty;
        public int PackageId { get; set; }
        public TourPackage? Tourpackage { get; set; }
    }
}
