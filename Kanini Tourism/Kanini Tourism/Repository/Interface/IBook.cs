using Kanini_Tourism.Models;

namespace Kanini_Tourism.Repository.Interface
{
    public interface IBook
    {
        IEnumerable<Booking> GetAllBooking();
        Booking GetBookingById(int User_Id);
        Task<List<Booking>> AddBooking(Booking user);
        Task<Booking?> UpdateBooking(int id, Booking updatedBooking);
        Task<List<Booking>?> DeleteBookingById(int id);
    }
}
