using Kanini_Tourism.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Repository.Interface
{
    public interface IGallery
    {
        IEnumerable<ImageGallery> GetAllImages();
        //Task<ImageGallery> ImageUpload([FromForm] ImageGallery doctor, IFormFile imageFile);
        Task<ImageGallery> ImageUpload(IFormFile imageFile);

    }
}
