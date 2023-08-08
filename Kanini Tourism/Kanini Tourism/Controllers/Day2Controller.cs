using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Kanini_Tourism.Repository.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Day2Controller : ControllerBase
    {

        private readonly IDay2Service _user;
        public Day2Controller(IDay2Service user)
        {
            _user = user;
        }
        [HttpGet]
        public IEnumerable<Day2> Get()
        {
            return _user.GetAllDay2();
        }
        [HttpGet("{id}")]
        public Day2 GetById(int id)
        {
            return _user.GetDay2ById(id);
        }

        //[HttpPost]
        //public async Task<ActionResult<List<Day2>>> Add(Day2 user)
        //{
        //    var users = await _user.AddDay2(user);
        //    return Ok(users);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Post(Day2 day2, IFormFile imageFile1, IFormFile imageFile2, IFormFile hotelImageFile1, IFormFile hotelImageFile2)
        //{
        //    try
        //    {
        //        if (day2 == null)
        //        {
        //            return BadRequest("Invalid request");
        //        }

        //        var createdDay2 = await _user.ImageUpload(imageFile1, imageFile2, hotelImageFile1, hotelImageFile2, day2);
        //        return CreatedAtAction("Post", createdDay2);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Day2 updatedDay2)
        {
            var existingDay2 = await _user.UpdateDay2(id, updatedDay2);
            if (existingDay2 == null)
            {
                return NotFound();
            }
            return Ok(existingDay2);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Day2>>> DeleteById(int id)
        {
            var users = await _user.DeleteDay2ById(id);
            if (users is null)
            {
                return NotFound("userid not matching");
            }
            return Ok(users);
        }
    }
}
