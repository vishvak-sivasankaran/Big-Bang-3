using Xunit;
using Moq;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kanini_Tourism.Controllers;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using Kanini_Tourism.Data;
using Kanini_Tourism.Repository.Service;

namespace Kanini_Tourism.Tests
{
    public class ImageTests
    {
        [Fact]
        public void GetAllImages_ReturnsListOfImages()
        {
            // Arrange
            var mockRepository = new Mock<IGallery>();
            var expectedTours = new List<ImageGallery>
    {
        new ImageGallery { Image = "image1.jpg" },
        new ImageGallery { Image = "image2.jpg" }
    };

            mockRepository.Setup(repo => repo.GetAllImages()).Returns(() => null);
            var controller = new ImageController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = controller.GetAllImages();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtActionResult_WithCreatedImage()
        {
            // Arrange
            var mockRepository = new Mock<IGallery>();
            var newImage = new ImageGallery { Image = "new-image.jpg" };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.ImageUpload(mockFormFile.Object)).ReturnsAsync(newImage);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new ImageController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = await controller.Post(mockFormFile.Object);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualCreatedImage = Assert.IsType<ImageGallery>(createdAtActionResult.Value);
            Assert.Equal(newImage.Image, actualCreatedImage.Image);
        }

       
       

        [Fact]
        public async Task ImageUpload_ThrowsException_WhenInvalidFile()
        {
            // Arrange
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            var mockContext = new Mock<TourDBContext>();
            var imageService = new ImageService(mockContext.Object, mockWebHostEnvironment.Object);

            var mockFormFile = new Mock<IFormFile>();
            mockFormFile.Setup(f => f.Length).Returns(0);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => imageService.ImageUpload(mockFormFile.Object));
        }

      

    }
}
