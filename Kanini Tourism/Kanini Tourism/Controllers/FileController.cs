using Kanini_Tourism.Data;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly TourDBContext db;
        public FileController(TourDBContext _db)
        {
            db = _db;
        }
      

        [HttpGet("GetImage")]
        public IActionResult GetImage()
        {
            try
            {
                string imagePath = "wwwroot/Gallery"; 
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);


                byte[] imageData = System.IO.File.ReadAllBytes(path);


                return File(imageData, "image/jpeg"); 
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetAllImages")]
        public IActionResult GetAllImages()
        {
            try
            {
                string imagesDirectory = Path.Combine("wwwroot", "Gallery");
                if (!Directory.Exists(imagesDirectory))
                {
                    return NotFound();
                }

                string[] imageFiles = Directory.GetFiles(imagesDirectory, "*.jpg"); // Change "*.jpg" to match your image file extension

                if (imageFiles.Length == 0)
                {
                    return NotFound();
                }

                List<string> imageFileNames = new List<string>();
                foreach (string imagePath in imageFiles)
                {
                    string fileName = Path.GetFileName(imagePath);
                    imageFileNames.Add(fileName);
                }

                return new JsonResult(imageFileNames);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("GetImage/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            try
            {
                string imagePath = Path.Combine("wwwroot", "Gallery", fileName);

                if (!System.IO.File.Exists(imagePath))
                {
                    return NotFound();
                }

                byte[] imageData = System.IO.File.ReadAllBytes(imagePath);

                return File(imageData, "image/jpeg");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }




        [HttpGet("GetImages")]
        public IActionResult GetImages()
        {
            try
            {
                string imagePath = Path.Combine("wwwroot", "Gallery");

                if (!Directory.Exists(imagePath))
                {
                    return NotFound(); 
                }

                string[] imageFiles = Directory.GetFiles(imagePath);

                if (imageFiles.Length == 0)
                {
                    return NotFound(); 
                }

                List<ImageData> imagesDataList = new List<ImageData>();

                foreach (string imageFile in imageFiles)
                {
                    // Determine the content type based on the file extension
                    string contentType = GetContentType(imageFile);

                    // Read the image bytes from the file
                    byte[] imageBytes = System.IO.File.ReadAllBytes(imageFile);

                    // Create an ImageData object to store the image data
                    ImageData imageData = new ImageData
                    {
                        ContentType = contentType,
                        ImageBytes = imageBytes
                    };

                    // Add the ImageData to the list
                    imagesDataList.Add(imageData);
                }

                // Return the list of images data as a JSON response
                return Ok(imagesDataList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private string GetContentType(string filePath)
        {
            // Get the file extension to determine the content type
            string extension = Path.GetExtension(filePath)?.ToLowerInvariant();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                default:
                    return "application/octet-stream";
            }
        }

        // Create a data model class to hold image data
        public class ImageData
        {
            public string ContentType { get; set; }
            public byte[] ImageBytes { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<Imagetbl>> PostImagetbl([FromForm] FileModel file)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Gallery", file.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.FormFile.CopyTo(stream);
                    Imagetbl imgtbl = new Imagetbl();
                    imgtbl.Imgname = "~/Gallery/" + file.FileName;
                    db.Imagetbls.Add(imgtbl);
                    await db.SaveChangesAsync();
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
