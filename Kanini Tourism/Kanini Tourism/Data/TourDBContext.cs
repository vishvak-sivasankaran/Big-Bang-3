using Kanini_Tourism.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanini_Tourism.Data
{
    public class TourDBContext:DbContext
    {
        public TourDBContext(DbContextOptions<TourDBContext> options):base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ImageGallery> ImageGallery { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Restaurent> Restaurents { get; set; }
        public DbSet<Spots> Spots { get; set; }
        public DbSet<Dummy> Dummy { get; set; }

        public virtual DbSet<Imagetbl> Imagetbls { get; set; }
        public TourDBContext()
        {
            // This constructor is needed for testing with Moq
        }


    }
}
