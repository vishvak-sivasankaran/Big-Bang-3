using Kanini_Tourism.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Repository.Interface
{
    public interface IUser
    {
        Task<User> AddUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsersByRole();
        Task<IEnumerable<User>> GetAllAgentByRole();

    }
}
