using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotel _user;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HotelController(IHotel user, IWebHostEnvironment webHostEnvironment)
        {
            _user = user;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetAllHotels()
        {
            var images = _user.GetAllHotels();
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Hotels>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Hotels");
                var filePath = Path.Combine(uploadsFolder, image.HotelImage);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);

                var tourPackageData = new Hotels
                {
                    HotelId = image.HotelId,
                    HotelName = image.HotelName,
                    Location = image.Location,
                    HotelImage = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(tourPackageData);
            }

            return new JsonResult(imageList);
        }

        [HttpGet("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var tourPackage = _user.GetHotelById(id);
            if (tourPackage == null)
            {
                return NotFound();
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Hotels");
            var filePath = Path.Combine(uploadsFolder, tourPackage.HotelImage);

            var imageBytes = System.IO.File.ReadAllBytes(filePath);

            var tourPackageData = new Hotels
            {
                HotelId = tourPackage.HotelId,
                HotelName = tourPackage.HotelName,
                Location = tourPackage.Location,
                HotelImage = Convert.ToBase64String(imageBytes)
            };

            return new JsonResult(tourPackageData);
        }

        [HttpPost]
        public async Task<ActionResult<Hotels>> Post([FromForm] Hotels hotel, IFormFile imageFile)
        {

            try
            {
                var createdHotel = await _user.CreateHotel(hotel, imageFile);
                return CreatedAtAction("Post", createdHotel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Hotels>> Put(int id, [FromForm] Hotels hotel, IFormFile imageFile)
        {
            try
            {
                hotel.HotelId = id;
                var updatedTour = await _user.UpdateHotel(hotel, imageFile);
                return Ok(updatedTour);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Hotels>>> DeleteHotelById(int id)
        {
            var users = await _user.DeleteHotelById(id);
            if (users is null)
            {
                return NotFound("package not matching");
            }
            return Ok(users);
        }

        // Destination
        [HttpGet("Location")]
        public IActionResult GetAllHotels(string location = null)
        {
            IEnumerable<Hotels> images;

            if (string.IsNullOrEmpty(location))
            {
                images = _user.GetAllHotels();
            }
            else
            {
                images = _user.FilterLocation(location);
            }

            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Hotels>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Hotels");
                var filePath = Path.Combine(uploadsFolder, image.HotelImage);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);

                var hotelData = new Hotels
                {
                    HotelId = image.HotelId,
                    HotelName = image.HotelName,
                    Location = image.Location,
                    HotelImage = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(hotelData);
            }

            return new JsonResult(imageList);
        }
    }
}
