using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _user;
        public BookController(IBook user)
        {
            _user = user;
        }
        [HttpGet]
        public IEnumerable<Booking> Get()
        {
            return _user.GetAllBooking();
        }
        [HttpGet("{id}")]
        public Booking GetById(int id)
        {
            return _user.GetBookingById(id);
        }

        [HttpPost]
        public async Task<ActionResult<List<Booking>>> Add(Booking user)
        {
            var users = await _user.AddBooking(user);
            return Ok(users);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Booking updatedBooking)
        {
            var existingBooking = await _user.UpdateBooking(id, updatedBooking);
            if (existingBooking == null)
            {
                return NotFound();
            }
            return Ok(existingBooking);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Booking>>> DeleteById(int id)
        {
            var users = await _user.DeleteBookingById(id);
            if (users is null)
            {
                return NotFound("userid not matching");
            }
            return Ok(users);
        }
    }
}
