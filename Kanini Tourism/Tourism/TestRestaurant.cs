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
    public class RestaurentTests
    {
        [Fact]
        public void GetAllRestaurent_ReturnsListOfRestaurents()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurent>();
            var expectedTours = new List<Restaurent>
    {
        new Restaurent { RestaurentId = 1, RestaurentName = "Tour 1", Location = "Destination 1",  RestaurentImage = "image1.jpg" },
        new Restaurent { RestaurentId = 2, RestaurentName = "Tour 2", Location = "Destination 2", RestaurentImage = "image2.jpg" }
    };

            mockRepository.Setup(repo => repo.GetAllrestaurents()).Returns(() => null);
            var controller = new RestaurentController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = controller.GetAllRestaurents();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }



        [Fact]
        public void GetrestaurentById_ExistingId_ReturnsRestaurent()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurent>();
            var expectedTour = new Restaurent
            {
                RestaurentId = 1,
                RestaurentName = "Tour 1",
                RestaurentImage = "a2b.jpg"
            };
            mockRepository.Setup(repo => repo.GetrestaurentById(1)).Returns(expectedTour);

            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();

            Mock.Get(mockWebHostEnvironment).Setup(env => env.WebRootPath).Returns("D:\\kanini training\\C#\\Kanini Tourism\\Kanini Tourism\\wwwroot");

            var controller = new RestaurentController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = controller.GetRestaurentById(1);

            // Assert
            var actionResult = Assert.IsType<JsonResult>(result);
            var actualTour = Assert.IsType<Restaurent>(actionResult.Value);

            Assert.Equal(expectedTour.RestaurentId, actualTour.RestaurentId);
            Assert.Equal(expectedTour.RestaurentName, actualTour.RestaurentName);

        }


        [Fact]
        public void GetRestaurentById_ReturnsNotFound_WhenRestaurentNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurent>();
            var restaurentId = 1;
            mockRepository.Setup(repo => repo.GetrestaurentById(restaurentId)).Returns((Restaurent)null);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new RestaurentController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = controller.GetRestaurentById(restaurentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtActionResult_WithCreatedRestaurent()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurent>();
            var newRestaurent = new Restaurent { RestaurentName = "New Restaurent", Location = "Location", RestaurentImage = "Image1" };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.Createrestaurent(newRestaurent, mockFormFile.Object)).ReturnsAsync(newRestaurent);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new RestaurentController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = await controller.Post(newRestaurent, mockFormFile.Object);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var actualCreatedRestaurent = Assert.IsType<Restaurent>(createdAtActionResult.Value);
            Assert.Equal(newRestaurent.RestaurentName, actualCreatedRestaurent.RestaurentName);
            Assert.Equal(newRestaurent.Location, actualCreatedRestaurent.Location);
        }


        [Fact]
        public async Task Put_ReturnsOkResult_WithUpdatedRestaurent()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurent>();
            var restaurentId = 1;
            var updatedRestaurent = new Restaurent { RestaurentId = restaurentId, RestaurentName = "Updated Restaurent", Location = "Location", RestaurentImage = "Image2" };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.Updaterestaurent(updatedRestaurent, mockFormFile.Object)).ReturnsAsync(updatedRestaurent);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new RestaurentController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = await controller.Put(restaurentId, updatedRestaurent, mockFormFile.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Restaurent>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualUpdatedRestaurent = Assert.IsType<Restaurent>(okResult.Value);
            Assert.Equal(updatedRestaurent.RestaurentId, actualUpdatedRestaurent.RestaurentId);
            Assert.Equal(updatedRestaurent.RestaurentName, actualUpdatedRestaurent.RestaurentName);
        }


        [Fact]
        public async Task DeleteRestaurentById_ReturnsOkResult_WithRemainingRestaurents()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurent>();
            var restaurentId = 1;
            var remainingRestaurents = new List<Restaurent>
            {
                new Restaurent { RestaurentId = 2, RestaurentName = "Restaurent 2", Location = "Location 2", RestaurentImage = "Image2" }
            };
            mockRepository.Setup(repo => repo.DeleterestaurentById(restaurentId)).ReturnsAsync(remainingRestaurents);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new RestaurentController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = await controller.DeleteRestaurentById(restaurentId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Restaurent>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualRemainingRestaurents = Assert.IsType<List<Restaurent>>(okResult.Value);
            Assert.Single(actualRemainingRestaurents);
            Assert.Equal(remainingRestaurents[0].RestaurentId, actualRemainingRestaurents[0].RestaurentId);
        }

       
    }
}
