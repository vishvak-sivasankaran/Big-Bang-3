using Kanini_Tourism.Data;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kanini_Tourism.Repository.Service
{
    public class BookingService:IBook
    {
        private readonly TourDBContext _UserContext;
        public BookingService(TourDBContext context)
        {
            _UserContext = context;
        }
        //GetAllBooking
        public IEnumerable<Booking> GetAllBooking()
        {
            return _UserContext.Bookings.ToList();
        }
        //GetBookingById
        public Booking GetBookingById(int User_Id)
        {
            return _UserContext.Bookings.FirstOrDefault(x => x.BookingId == User_Id);
        }
        //Post
        public async Task<List<Booking>> AddBooking(Booking user)
        {
            _UserContext.Bookings.Add(user);
            await _UserContext.SaveChangesAsync();

            return await _UserContext.Bookings.ToListAsync();
        }
        //Put
        public async Task<Booking?> UpdateBooking(int id, Booking updatedBooking)
        {
            var existingBooking = await _UserContext.Bookings.FindAsync(id);
            if (existingBooking == null)
            {
                return null;
            }

            existingBooking.Name = updatedBooking.Name;
            existingBooking.Email = updatedBooking.Email;
            existingBooking.StartDate = updatedBooking.StartDate;
            existingBooking.Adult = updatedBooking.Adult;
            existingBooking.Child = updatedBooking.Child;
            existingBooking.TotalPrice = updatedBooking.TotalPrice;
 
            await _UserContext.SaveChangesAsync();

            return existingBooking;
        }

        //Delete
        public async Task<List<Booking>?> DeleteBookingById(int id)
        {
            var users = await _UserContext.Bookings.FindAsync(id);
            if (users is null)
            {
                return null;
            }
            _UserContext.Remove(users);
            await _UserContext.SaveChangesAsync();
            return await _UserContext.Bookings.ToListAsync();
        }
       
    }
}
