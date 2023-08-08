using Kanini_Tourism.Data;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kanini_Tourism.Repository.Service
{
    public class RestaurentService:IRestaurent
    {
        private readonly TourDBContext _UserContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RestaurentService(TourDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _UserContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        //GetAllrestaurents
        public IEnumerable<Restaurent> GetAllrestaurents()
        {
            return _UserContext.Restaurents.ToList();
        }
        //GetrestaurentById
        public Restaurent GetrestaurentById(int User_Id)
        {
            return _UserContext.Restaurents.FirstOrDefault(x => x.RestaurentId == User_Id);
        }
        //Post
        public async Task<Restaurent> Createrestaurent([FromForm] Restaurent restaurent, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Restaurents");
            var fileName = imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            restaurent.RestaurentImage = fileName;

            _UserContext.Restaurents.Add(restaurent);
            await _UserContext.SaveChangesAsync();

            return restaurent;
        }

        //update

        public async Task<Restaurent> Updaterestaurent(Restaurent restaurent, IFormFile imageFile)
        {

            var existingrestaurent = await _UserContext.Restaurents.FindAsync(restaurent.PackageId);

            if (existingrestaurent == null)
            {
                throw new ArgumentException("restaurent not found");
            }

            existingrestaurent.RestaurentName = restaurent.RestaurentName;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Restaurents");
                var fileName = imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingrestaurent.RestaurentImage = fileName;

            }

            await _UserContext.SaveChangesAsync();

            return existingrestaurent;
        }

        //Delete
        public async Task<List<Restaurent>?> DeleterestaurentById(int id)
        {
            var users = await _UserContext.Restaurents.FindAsync(id);
            if (users is null)
            {
                return null;
            }
            _UserContext.Remove(users);
            await _UserContext.SaveChangesAsync();
            return await _UserContext.Restaurents.ToListAsync();
        }
        //Filtering location
        public IEnumerable<Restaurent> FilterLocation(string Location)
        {
            try
            {
                var query = _UserContext.Restaurents.AsQueryable();

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
