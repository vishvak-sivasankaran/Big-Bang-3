using MakeYourTripAPI.Models;

namespace MakeYourTripAPI.Repository.Interface
{
    public interface IUser
    {
        Task<User> AddUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
    }
}
