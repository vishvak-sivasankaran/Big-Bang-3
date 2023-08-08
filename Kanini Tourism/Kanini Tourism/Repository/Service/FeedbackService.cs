using Kanini_Tourism.Data;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Kanini_Tourism.Repository.Service
{
    public class FeedbackService:IFeedback
    {
        private readonly TourDBContext _UserContext;
        public FeedbackService(TourDBContext context)
        {
            _UserContext = context;
        }
        //GetAllFeedback
        public IEnumerable<Feedback> GetAllFeedback()
        {
            return _UserContext.Feedbacks.ToList();
        }
        //GetFeedbackById
        public Feedback GetFeedbackById(int User_Id)
        {
            return _UserContext.Feedbacks.FirstOrDefault(x => x.FeedId == User_Id);
        }
        //Post
        public async Task<List<Feedback>> AddFeedback(Feedback user)
        {
            _UserContext.Feedbacks.Add(user);
            await _UserContext.SaveChangesAsync();

            return await _UserContext.Feedbacks.ToListAsync();
        }
        //Put
        public async Task<Feedback?> UpdateFeedback(int id, Feedback updatedFeedback)
        {
            var existingFeedback = await _UserContext.Feedbacks.FindAsync(id);
            if (existingFeedback == null)
            {
                return null;
            }

            existingFeedback.Name = updatedFeedback.Name;
            existingFeedback.Email = updatedFeedback.Email;
            existingFeedback.Description = updatedFeedback.Description; 
            existingFeedback.Rating = updatedFeedback.Rating;

            await _UserContext.SaveChangesAsync();

            return existingFeedback;
        }


        //Delete
        public async Task<List<Feedback>?> DeleteFeedbackById(int id)
        {
            var users = await _UserContext.Feedbacks.FindAsync(id);
            if (users is null)
            {
                return null;
            }
            _UserContext.Remove(users);
            await _UserContext.SaveChangesAsync();
            return await _UserContext.Feedbacks.ToListAsync();
        }
    }
}
