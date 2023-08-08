using Kanini_Tourism.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Repository.Interface
{
    public interface ISpot
    {
        IEnumerable<Spots> GetAllSpots();
        Spots GetSpotsById(int User_Id);
        Task<Spots> CreateSpots([FromForm] Spots Spots, IFormFile imageFile);
        Task<Spots> UpdateSpots(Spots tour, IFormFile imageFile);
        Task<List<Spots>?> DeleteSpotsById(int id);
        IEnumerable<Spots> FilterLocation(string Location);
    }
}
