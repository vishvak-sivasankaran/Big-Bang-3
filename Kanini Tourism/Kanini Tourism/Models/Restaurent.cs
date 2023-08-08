using System.ComponentModel.DataAnnotations;

namespace Kanini_Tourism.Models
{
    public class Restaurent
    {
        [Key]
        public int RestaurentId { get; set; }
        public string RestaurentName { get; set; } = string.Empty;
        public string RestaurentImage { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int PackageId { get; set; }
        public TourPackage? Tourpackage { get; set; }
    }
}
