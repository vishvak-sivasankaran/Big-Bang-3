using Kanini_Tourism.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Repository.Interface
{
    public interface IRestaurent
    {
        IEnumerable<Restaurent> GetAllrestaurents();
        Restaurent GetrestaurentById(int User_Id);
        Task<Restaurent> Createrestaurent([FromForm] Restaurent restaurent, IFormFile imageFile);
        Task<Restaurent> Updaterestaurent(Restaurent restaurent, IFormFile imageFile);
        Task<List<Restaurent>?> DeleterestaurentById(int id);
        IEnumerable<Restaurent> FilterLocation(string Location);
    }
}
