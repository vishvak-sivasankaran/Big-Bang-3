using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotController : ControllerBase
    {
        private readonly ISpot _user;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SpotController(ISpot user, IWebHostEnvironment webHostEnvironment)
        {
            _user = user;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetAllSpots()
        {
            var images = _user.GetAllSpots();
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Spots>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Spots");
                var filePath = Path.Combine(uploadsFolder, image.SpotImage);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);

                var tourPackageData = new Spots
                {
                    SpotId = image.SpotId,
                    SpotName = image.SpotName,
                    Location = image.Location,
                    SpotImage = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(tourPackageData);
            }

            return new JsonResult(imageList);
        }

        [HttpGet("{id}")]
        public IActionResult GetSpotById(int id)
        {
            var tourPackage = _user.GetSpotsById(id);
            if (tourPackage == null)
            {
                return NotFound();
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Spots");
            var filePath = Path.Combine(uploadsFolder, tourPackage.SpotImage);

            var imageBytes = System.IO.File.ReadAllBytes(filePath);

            var tourPackageData = new Spots
            {
                SpotId = tourPackage.SpotId,
                SpotName = tourPackage.SpotName,
                Location = tourPackage.Location,
                SpotImage = Convert.ToBase64String(imageBytes)
            };

            return new JsonResult(tourPackageData);
        }

        [HttpPost]
        public async Task<ActionResult<Spots>> Post([FromForm] Spots Spots, IFormFile imageFile)
        {

            try
            {
                var createdSpots = await _user.CreateSpots(Spots, imageFile);
                return CreatedAtAction("Post", createdSpots);

            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Spots>> Put(int id, [FromForm] Spots Spots, IFormFile imageFile)
        {
            try
            {
                Spots.SpotId = id;
                var updatedTour = await _user.UpdateSpots(Spots, imageFile);
                return Ok(updatedTour);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Spots>>> DeleteSpotsById(int id)
        {
            var users = await _user.DeleteSpotsById(id);
            if (users is null)
            {
                return NotFound("package not matching");
            }
            return Ok(users);
        }

        // Destination
        [HttpGet("Location")]
        public IActionResult GetAllSpots(string location = null)
        {
            IEnumerable<Spots> images;

            if (string.IsNullOrEmpty(location))
            {
                images = _user.GetAllSpots();
            }
            else
            {
                images = _user.FilterLocation(location);
            }

            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Spots>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Spots");
                var filePath = Path.Combine(uploadsFolder, image.SpotImage);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);

                var hotelData = new Spots
                {
                    SpotId = image.SpotId,
                    SpotName = image.SpotName,
                    Location = image.Location,
                    SpotImage = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(hotelData);
            }

            return new JsonResult(imageList);
        }
    }
    
}

