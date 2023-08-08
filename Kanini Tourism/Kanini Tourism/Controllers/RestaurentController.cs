using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurentController : ControllerBase
    {
         private readonly IRestaurent _user;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RestaurentController(IRestaurent user, IWebHostEnvironment webHostEnvironment)
        {
            _user = user;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetAllRestaurents()
        {
            var images = _user.GetAllrestaurents();
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Restaurent>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Restaurents");
                var filePath = Path.Combine(uploadsFolder, image.RestaurentImage);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);

                var tourPackageData = new Restaurent
                {
                    RestaurentId = image.RestaurentId,
                    RestaurentName = image.RestaurentName,
                    Location = image.Location,
                    RestaurentImage = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(tourPackageData);
            }

            return new JsonResult(imageList);
        }
        [HttpGet("{id}")]
        public IActionResult GetRestaurentById(int id)
        {
            var tourPackage = _user.GetrestaurentById(id);
            if (tourPackage == null)
            {
                return NotFound();
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Restaurents");
            var filePath = Path.Combine(uploadsFolder, tourPackage.RestaurentImage);

            var imageBytes = System.IO.File.ReadAllBytes(filePath);

            var tourPackageData = new Restaurent
            {
                RestaurentId = tourPackage.RestaurentId,
                RestaurentName = tourPackage.RestaurentName,
                Location = tourPackage.Location,
                RestaurentImage = Convert.ToBase64String(imageBytes)
            };

            return new JsonResult(tourPackageData);
        }

        [HttpPost]
        public async Task<ActionResult<Restaurent>> Post([FromForm] Restaurent restaurent, IFormFile imageFile)
        {

            try
            {
                var createdRestaurent = await _user.Createrestaurent(restaurent, imageFile);
                return CreatedAtAction("Post", createdRestaurent);

            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Restaurent>> Put(int id, [FromForm] Restaurent restaurent, IFormFile imageFile)
        {
            try
            {
                restaurent.RestaurentId = id;
                var updatedTour = await _user.Updaterestaurent(restaurent, imageFile);
                return Ok(updatedTour);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Restaurent>>> DeleteRestaurentById(int id)
        {
            var users = await _user.DeleterestaurentById(id);
            if (users is null)
            {
                return NotFound("package not matching");
            }
            return Ok(users);
        }

        // Destination

        [HttpGet("Location")]
        public IActionResult GetAllRestaurents(string location = null)
        {
            IEnumerable<Restaurent> images;

            if (string.IsNullOrEmpty(location))
            {
                images = _user.GetAllrestaurents();
            }
            else
            {
                images = _user.FilterLocation(location);
            }

            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Restaurent>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Restaurents");
                var filePath = Path.Combine(uploadsFolder, image.RestaurentImage);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);

                var hotelData = new Restaurent
                {
                    RestaurentId = image.RestaurentId,
                    RestaurentName = image.RestaurentName,
                    Location = image.Location,
                    RestaurentImage = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(hotelData);
            }

            return new JsonResult(imageList);
        }
    }
}
