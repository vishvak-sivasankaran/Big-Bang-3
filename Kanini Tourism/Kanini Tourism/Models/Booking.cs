using System.ComponentModel.DataAnnotations;

namespace Kanini_Tourism.Models
{
    public class Booking
    {
        [Key]

        public int BookingId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double Phone_Number { get;set; }
        public DateTime StartDate { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int TotalPrice { get; set; }
        public int PackageId { get; set; }
        public TourPackage? TourPackage { get; set; }
        public int HotelId { get; set; }
        public Hotels? Hotels { get; set; }
        public int RestaurentId { get; set; }
        public Restaurent? Restaurents { get; set; }

    }
}
