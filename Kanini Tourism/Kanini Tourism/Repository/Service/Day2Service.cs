using Kanini_Tourism.Data;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Kanini_Tourism.Repository.Service
{
    public class Day2Service:IDay2Service
    {
        private readonly TourDBContext _UserContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Day2Service(TourDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _UserContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        //GetAll
        public IEnumerable<Day2> GetAllDay2()
        {
            return _UserContext.Daytwo.ToList();
        }
        //GetById
        public Day2 GetDay2ById(int User_Id)
        {
            return _UserContext.Daytwo.FirstOrDefault(x => x.Pack_Id == User_Id);
        }

        //Post
        //public async Task<List<Day2>> AddDay2(Day2 user)
        //{
        //    _UserContext.Daytwo.Add(user);
        //    await _UserContext.SaveChangesAsync();

        //    return await _UserContext.Daytwo.ToListAsync();
        //}

      
      


    // PUT 
    public async Task<Day2?> UpdateDay2(int id, Day2 updatedDay2)
        {
            var existingDay2 = await _UserContext.Daytwo.FindAsync(id);
            if (existingDay2 == null)
            {
                return null; 
            }

            existingDay2.Pack_Name = updatedDay2.Pack_Name;
            existingDay2.Location = updatedDay2.Location;
            existingDay2.From = updatedDay2.From;
            existingDay2.Cost_per_person = updatedDay2.Cost_per_person;
            existingDay2.Day1_Locations = updatedDay2.Day1_Locations;
            existingDay2.Day2_Locations = updatedDay2.Day2_Locations;
            existingDay2.Day1_Hotel= updatedDay2.Day1_Hotel;
            existingDay2.Day2_Hotel= updatedDay2.Day2_Hotel;
            existingDay2.Day1_Description = updatedDay2.Day1_Description;
            existingDay2.Day2_Description = updatedDay2.Day2_Description;
            await _UserContext.SaveChangesAsync();

            return existingDay2;
        }

        //Delete
        public async Task<List<Day2>?> DeleteDay2ById(int id)
        {
            var users = await _UserContext.Daytwo.FindAsync(id);
            if (users is null)
            {
                return null;
            }
            _UserContext.Remove(users);
            await _UserContext.SaveChangesAsync();
            return await _UserContext.Daytwo.ToListAsync();
        }
    }
}
