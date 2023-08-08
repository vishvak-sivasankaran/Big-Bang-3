using System.ComponentModel.DataAnnotations;

namespace Kanini_Tourism.Models
{
    public class Spots
    {
        [Key]
        public int SpotId { get; set; }

        public string SpotName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
        public string SpotImage { get; set; } = string.Empty;

        public int PackageId { get; set; }
        public TourPackage? Tourpackage { get; set; }
    }
}
