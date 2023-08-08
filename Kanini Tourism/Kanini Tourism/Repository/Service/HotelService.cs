using Kanini_Tourism.Data;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kanini_Tourism.Repository.Service
{
    public class HotelService:IHotel
    {
        private readonly TourDBContext _UserContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HotelService(TourDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _UserContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        //GetAllHotels
        public IEnumerable<Hotels> GetAllHotels()
        {
            return _UserContext.Hotels.ToList();
        }
        //GetHotelById
        public Hotels GetHotelById(int User_Id)
        {
            return _UserContext.Hotels.FirstOrDefault(x => x.HotelId == User_Id);
        }
        //Post
        public async Task<Hotels> CreateHotel([FromForm] Hotels Hotel, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Hotels");
            var fileName = imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            Hotel.HotelImage = fileName;

            _UserContext.Hotels.Add(Hotel);
            await _UserContext.SaveChangesAsync();

            return Hotel;
        }

        //update

        public async Task<Hotels> UpdateHotel(Hotels Hotel, IFormFile imageFile)
        {

            var existingHotel = await _UserContext.Hotels.FindAsync(Hotel.PackageId);

            if (existingHotel == null)
            {
                throw new ArgumentException("Hotel not found");
            }

            existingHotel.HotelName = Hotel.HotelName;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Hotels");
                var fileName = imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingHotel.HotelImage = fileName;

            }

            await _UserContext.SaveChangesAsync();

            return existingHotel;
        }

        //Delete
        public async Task<List<Hotels>?> DeleteHotelById(int id)
        {
            var users = await _UserContext.Hotels.FindAsync(id);
            if (users is null)
            {
                return null;
            }
            _UserContext.Remove(users);
            await _UserContext.SaveChangesAsync();
            return await _UserContext.Hotels.ToListAsync();
        }
        //Filtering location
        public IEnumerable<Hotels> FilterLocation(string Location)
        {
            try
            {
                var query = _UserContext.Hotels.AsQueryable();

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
