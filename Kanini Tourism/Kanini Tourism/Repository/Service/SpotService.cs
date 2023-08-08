using Kanini_Tourism.Data;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kanini_Tourism.Repository.Service
{
    public class SpotService:ISpot
    {
        private readonly TourDBContext _UserContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SpotService(TourDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _UserContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        //GetAllSpots
        public IEnumerable<Spots> GetAllSpots()
        {
            return _UserContext.Spots.ToList();
        }
        //GetSpotsById
        public Spots GetSpotsById(int User_Id)
        {
            return _UserContext.Spots.FirstOrDefault(x => x.SpotId == User_Id);
        }
        //Post
        public async Task<Spots> CreateSpots([FromForm] Spots Spots, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Spots");
            var fileName = imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            Spots.SpotImage = fileName;

            _UserContext.Spots.Add(Spots);
            await _UserContext.SaveChangesAsync();

            return Spots;
        }

        //update

        public async Task<Spots> UpdateSpots(Spots Spots, IFormFile imageFile)
        {

            var existingSpots = await _UserContext.Spots.FindAsync(Spots.PackageId);

            if (existingSpots == null)
            {
                throw new ArgumentException("Spots not found");
            }

            existingSpots.SpotName = Spots.SpotName;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Spots");
                var fileName = imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingSpots.SpotImage = fileName;

            }

            await _UserContext.SaveChangesAsync();

            return existingSpots;
        }

        //Delete
        public async Task<List<Spots>?> DeleteSpotsById(int id)
        {
            var users = await _UserContext.Spots.FindAsync(id);
            if (users is null)
            {
                return null;
            }
            _UserContext.Remove(users);
            await _UserContext.SaveChangesAsync();
            return await _UserContext.Spots.ToListAsync();
        }
        //Filtering location
        public IEnumerable<Spots> FilterLocation(string Location)
        {
            try
            {
                var query = _UserContext.Spots.AsQueryable();

                if (!string.IsNullOrEmpty(Location))
                {
                    query = query.Where(h => h.Location.Contains(Location));
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
