using MakeYourTripAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTripAPI.Data
{
    public class TourDBContext : DbContext
    {
        public TourDBContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
