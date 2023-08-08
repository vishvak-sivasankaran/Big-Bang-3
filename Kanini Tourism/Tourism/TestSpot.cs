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
using System.Linq;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System;

namespace Kanini_Tourism.Tests
{
    public class SpotTests
    {
        [Fact]
        public void GetAllSpot_ReturnsListOfSpots()
        {
            // Arrange
            var mockRepository = new Mock<ISpot>();
            var expectedTours = new List<Spots>
    {
        new Spots { SpotId = 1, SpotName = "Tour 1", Location = "Destination 1",  SpotImage = "image1.jpg" },
        new Spots { SpotId = 2, SpotName = "Tour 2", Location = "Destination 2", SpotImage = "image2.jpg" }
    };

            mockRepository.Setup(repo => repo.GetAllSpots()).Returns(() => null);
            var controller = new SpotController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = controller.GetAllSpots();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }



        [Fact]
        public void GetSpotById_ExistingId_ReturnsSpot()
        {
            // Arrange
            var mockRepository = new Mock<ISpot>();
            var expectedTour = new Spots
            {
                SpotId = 1,
                SpotName = "Tour 1",
                SpotImage = "glassbridge.jpg"
            };
            mockRepository.Setup(repo => repo.GetSpotsById(1)).Returns(expectedTour);

            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();

            Mock.Get(mockWebHostEnvironment).Setup(env => env.WebRootPath).Returns("D:\\kanini training\\C#\\Kanini Tourism\\Kanini Tourism\\wwwroot");

            var controller = new SpotController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = controller.GetSpotById(1);

            // Assert
            var actionResult = Assert.IsType<JsonResult>(result);
            var actualTour = Assert.IsType<Spots>(actionResult.Value);

            Assert.Equal(expectedTour.SpotId, actualTour.SpotId);
            Assert.Equal(expectedTour.SpotName, actualTour.SpotName);

        }


        [Fact]
        public void GetSpotById_ReturnsNotFound_WhenSpotNotFound()
        {
            // Arrange
            var mockRepository = new Mock<ISpot>();
            var SpotId = 1;
            mockRepository.Setup(repo => repo.GetSpotsById(SpotId)).Returns((Spots)null);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new SpotController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = controller.GetSpotById(SpotId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtActionResult_WithCreatedSpot()
        {
            // Arrange
            var mockRepository = new Mock<ISpot>();
            var newSpot = new Spots { SpotName = "New Spot", Location = "Location", SpotImage = "Image1" };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.CreateSpots(newSpot, mockFormFile.Object)).ReturnsAsync(newSpot);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new SpotController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = await controller.Post(newSpot, mockFormFile.Object);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var actualCreatedSpot = Assert.IsType<Spots>(createdAtActionResult.Value);
            Assert.Equal(newSpot.SpotName, actualCreatedSpot.SpotName);
            Assert.Equal(newSpot.Location, actualCreatedSpot.Location);
        }


        [Fact]
        public async Task Put_ReturnsOkResult_WithUpdatedSpot()
        {
            // Arrange
            var mockRepository = new Mock<ISpot>();
            var SpotId = 1;
            var updatedSpot = new Spots { SpotId = SpotId, SpotName = "Updated Spot", Location = "Location", SpotImage = "Image2" };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.UpdateSpots(updatedSpot, mockFormFile.Object)).ReturnsAsync(updatedSpot);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new SpotController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = await controller.Put(SpotId, updatedSpot, mockFormFile.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Spots>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualUpdatedSpot = Assert.IsType<Spots>(okResult.Value);
            Assert.Equal(updatedSpot.SpotId, actualUpdatedSpot.SpotId);
            Assert.Equal(updatedSpot.SpotName, actualUpdatedSpot.SpotName);
        }


        [Fact]
        public async Task DeleteSpotById_ReturnsOkResult_WithRemainingSpots()
        {
            // Arrange
            var mockRepository = new Mock<ISpot>();
            var SpotId = 1;
            var remainingSpots = new List<Spots>
            {
                new Spots { SpotId = 2, SpotName = "Spot 2", Location = "Location 2", SpotImage = "Image2" }
            };
            mockRepository.Setup(repo => repo.DeleteSpotsById(SpotId)).ReturnsAsync(remainingSpots);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new SpotController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = await controller.DeleteSpotsById(SpotId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Spots>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualRemainingSpots = Assert.IsType<List<Spots>>(okResult.Value);
            Assert.Single(actualRemainingSpots);
            Assert.Equal(remainingSpots[0].SpotId, actualRemainingSpots[0].SpotId);
        }


    }
}
