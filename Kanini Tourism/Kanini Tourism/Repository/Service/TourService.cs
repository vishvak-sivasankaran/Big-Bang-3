using Kanini_Tourism.Data;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kanini_Tourism.Repository.Service
{
    public class TourService:ITour
    {
        private readonly TourDBContext _UserContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TourService(TourDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _UserContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        //GetAllTours
        public IEnumerable<TourPackage> GetAllTours()
        {
            return _UserContext.TourPackages.ToList();
        }
        //GetTourById
        public TourPackage GetTourById(int User_Id)
        {
            return _UserContext.TourPackages.FirstOrDefault(x => x.PackageId == User_Id);
        }
        //Post
        public async Task<TourPackage> CreateTour([FromForm] TourPackage tour, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Packages");
            var fileName = imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            tour.PackImage = fileName;

            _UserContext.TourPackages.Add(tour);
            await _UserContext.SaveChangesAsync();

            return tour;
        }

        //update

        public async Task<TourPackage> UpdateTour(TourPackage tour, IFormFile imageFile)
        {
           
            var existingTour = await _UserContext.TourPackages.FindAsync(tour.PackageId);

            if (existingTour == null)
            {
                throw new ArgumentException("Package not found");
            }

            existingTour.PackageName = tour.PackageName;
            existingTour.Destination = tour.Destination;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Packages");
                var fileName = imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingTour.PackImage = fileName;

            }

            await _UserContext.SaveChangesAsync();

            return existingTour;
        }

        //Delete
        public async Task<List<TourPackage>?> DeleteTourById(int id)
        {
            var users = await _UserContext.TourPackages.FindAsync(id);
            if (users is null)
            {
                return null;
            }
            _UserContext.Remove(users);
            await _UserContext.SaveChangesAsync();
            return await _UserContext.TourPackages.ToListAsync();
        }
        //Filtering location
        public IEnumerable<TourPackage> FilterLocation(string Destination)
        {
            try
            {
                var query = _UserContext.TourPackages.AsQueryable();

                if (!string.IsNullOrEmpty(Destination))
                {
                    query = query.Where(h => h.Destination.Contains(Destination));
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while filtering the Location.", ex);
            }
        }
    }
}
