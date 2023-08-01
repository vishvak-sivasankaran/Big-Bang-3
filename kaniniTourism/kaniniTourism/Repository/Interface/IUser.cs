using kaniniTourism.Models;

namespace kaniniTourism.Repository.Interface
{
    public interface IUser
    {
        Task<User> AddUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
    }
}
