using System.ComponentModel.DataAnnotations;

namespace Kanini_Tourism.Models
{
    public partial class Imagetbl
    {
        [Key]
        public int Imgid { get; set; }

        public string? Imgname { get; set; }


    }
}
