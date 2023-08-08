using Kanini_Tourism.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Repository.Interface
{
    public interface ITour
    {
        IEnumerable<TourPackage> GetAllTours();
        TourPackage GetTourById(int User_Id);
        Task<TourPackage> CreateTour([FromForm] TourPackage tour, IFormFile imageFile);
        Task<TourPackage> UpdateTour(TourPackage tour, IFormFile imageFile);
        Task<List<TourPackage>?> DeleteTourById(int id);
        IEnumerable<TourPackage> FilterLocation(string Destination);
    }
}
