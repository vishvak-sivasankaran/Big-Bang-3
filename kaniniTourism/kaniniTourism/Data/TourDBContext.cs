using kaniniTourism.Models;
using Microsoft.EntityFrameworkCore;

namespace kaniniTourism.Data
{
    public class TourDBContext:DbContext
    {
        public TourDBContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
