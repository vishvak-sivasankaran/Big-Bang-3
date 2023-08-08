using Kanini_Tourism.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Repository.Interface
{
    public interface IHotel
    {
        IEnumerable<Hotels> GetAllHotels();
        Hotels GetHotelById(int User_Id);
        Task<Hotels> CreateHotel([FromForm] Hotels hotel, IFormFile imageFile);
        Task<Hotels> UpdateHotel(Hotels tour, IFormFile imageFile);
        Task<List<Hotels>?> DeleteHotelById(int id);
        IEnumerable<Hotels> FilterLocation(string Location);
    }
}
