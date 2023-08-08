using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Kanini_Tourism.Repository.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Kanini_Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITour _user;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TourController(ITour user, IWebHostEnvironment webHostEnvironment)
        {
            _user = user;
            _webHostEnvironment = webHostEnvironment;
        }
      
  
        [HttpGet]
        public IActionResult GetAllImages()
        {
            var images = _user.GetAllTours();
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<TourPackage>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Packages");
                var filePath = Path.Combine(uploadsFolder, image.PackImage);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);

                var tourPackageData = new TourPackage
                {
                    PackageId = image.PackageId,
                    PackageName = image.PackageName,
                    Destination = image.Destination,
                    PriceForAdult = image.PriceForAdult,
                    PriceForChild = image.PriceForChild,
                    Duration = image.Duration,
                    Description = image.Description,
                    PackImage = Convert.ToBase64String(imageBytes) 
                };

                imageList.Add(tourPackageData);
            }

            return new JsonResult(imageList);
        }


        [HttpGet("{id}")]
        public IActionResult GetTourById(int id)
        {
            var tourPackage = _user.GetTourById(id);
            if (tourPackage == null)
            {
                return NotFound();
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Packages");
            var filePath = Path.Combine(uploadsFolder, tourPackage.PackImage);

            var imageBytes = System.IO.File.ReadAllBytes(filePath);

            var tourPackageData = new TourPackage
            {
                PackageId = tourPackage.PackageId,
                PackageName = tourPackage.PackageName,
                Destination = tourPackage.Destination,
                PriceForAdult = tourPackage.PriceForAdult,
                PriceForChild = tourPackage.PriceForChild,
                Duration = tourPackage.Duration,
                Description = tourPackage.Description,
                PackImage = Convert.ToBase64String(imageBytes)
            };

            return new JsonResult(tourPackageData);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] TourPackage tour, IFormFile imageFile)
        {
            try
            {
                var uploadedImage = await _user.CreateTour(tour, imageFile);
                return CreatedAtAction("Post", uploadedImage);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TourPackage>> Put(int id, [FromForm] TourPackage tour, IFormFile imageFile)
        {
            try
            {
                tour.PackageId = id;
                var updatedTour = await _user.UpdateTour(tour, imageFile);
                return Ok(updatedTour);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TourPackage>>> DeleteTourById(int id)
        {
            var users = await _user.DeleteTourById(id);
            if (users is null)
            {
                return NotFound("package not matching");
            }
            return Ok(users);
        }


        [HttpGet("Location")]
        public IActionResult GetAllImages(string destination = null)
        {
            IEnumerable<TourPackage> images;

            if (string.IsNullOrEmpty(destination))
            {
                images = _user.GetAllTours();
            }
            else
            {
                images = _user.FilterLocation(destination);
            }

            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<TourPackage>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Packages");
                var filePath = Path.Combine(uploadsFolder, image.PackImage);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);

                var tourPackageData = new TourPackage
                {
                    PackageId = image.PackageId,
                    PackageName = image.PackageName,
                    Destination = image.Destination,
                    PriceForAdult = image.PriceForAdult,
                    PriceForChild = image.PriceForChild,
                    Duration = image.Duration,
                    Description = image.Description,
                    PackImage = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(tourPackageData);
            }

            return new JsonResult(imageList);
        }

    }
}
