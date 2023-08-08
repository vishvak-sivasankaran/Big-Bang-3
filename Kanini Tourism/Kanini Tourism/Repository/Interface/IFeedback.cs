using Kanini_Tourism.Models;

namespace Kanini_Tourism.Repository.Interface
{
    public interface IFeedback
    {
        IEnumerable<Feedback> GetAllFeedback();
        Feedback GetFeedbackById(int User_Id);
        Task<List<Feedback>> AddFeedback(Feedback user);
        Task<Feedback?> UpdateFeedback(int id, Feedback updatedFeedback);
        Task<List<Feedback>?> DeleteFeedbackById(int id);
    }
}
