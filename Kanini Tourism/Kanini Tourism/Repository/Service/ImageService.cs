using Kanini_Tourism.Data;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kanini_Tourism.Repository.Service
{
    public class ImageService : IGallery
    {
        private readonly TourDBContext _UserContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(TourDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _UserContext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GetAllImages
        public IEnumerable<ImageGallery> GetAllImages()
        {
            return _UserContext.ImageGallery.ToList();
        }

        // Post
        public async Task<ImageGallery> ImageUpload(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Gallery");
            var fileName = imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            var image = new ImageGallery
            {
                Image = fileName
            };

            _UserContext.ImageGallery.Add(image);
            await _UserContext.SaveChangesAsync();

            return image;
        }

        
    }
}
