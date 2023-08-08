using Kanini_Tourism.Models;

namespace Kanini_Tourism.Repository.Interface
{
    public interface IDay2Service
    {
        IEnumerable<Day2> GetAllDay2();
        Day2 GetDay2ById(int User_Id);
        //Task<List<Day2>> AddDay2(Day2 user);
         //Task<Day2> ImageUpload(IFormFile imageFile1, IFormFile imageFile2, IFormFile hotelImageFile1, IFormFile hotelImageFile2, Day2 day2);
       
        Task<Day2?> UpdateDay2(int id, Day2 updatedDay2);
        Task<List<Day2>?> DeleteDay2ById(int id);

    }
}
