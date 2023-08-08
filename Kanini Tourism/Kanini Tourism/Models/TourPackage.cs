using System.ComponentModel.DataAnnotations;

namespace Kanini_Tourism.Models
{
    public class TourPackage
    {
        [Key]
        public int PackageId { get; set; }
        public string PackageName { get; set; }=string.Empty;
        public string Destination { get; set; } = string.Empty;
        public int PriceForAdult { get; set; }
        public int PriceForChild { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public string PackImage { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public ICollection<Hotels>? Hotels { get; set; }
        public ICollection<Restaurent>? Restaurents { get; set; }
        public ICollection<Spots>? Nearbyspots { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
