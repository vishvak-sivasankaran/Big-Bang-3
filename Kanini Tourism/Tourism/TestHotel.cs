using Xunit;
using Moq;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kanini_Tourism.Controllers;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using System.IO;
using System;
using Microsoft.Extensions.FileProviders;

namespace Kanini_Tourism.Tests
{
    public class HotelTests
    {
       

        [Fact]
        public void GetAllImages_ReturnsListOfHotels()
        {
            // Arrange
            var mockRepository = new Mock<IHotel>();
            var expectedTours = new List<Hotels>
    {
        new Hotels { HotelId = 1, HotelName = "Tour 1", Location = "Destination 1",  HotelImage = "image1.jpg" },
        new Hotels { HotelId = 2, HotelName = "Tour 2", Location = "Destination 2", HotelImage = "image2.jpg" }
    };
           
            mockRepository.Setup(repo => repo.GetAllHotels()).Returns(() => null);
            var controller = new HotelController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = controller.GetAllHotels();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        [Fact]
        public void GetHotelById_ExistingId_ReturnsHotel()
        {
            // Arrange
            var mockRepository = new Mock<IHotel>();
            var expectedTour = new Hotels
            {
                HotelId = 1,
                HotelName = "Tour 1",
                HotelImage = "accord.jpg"
            };
            mockRepository.Setup(repo => repo.GetHotelById(1)).Returns(expectedTour);

            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
   
            Mock.Get(mockWebHostEnvironment).Setup(env => env.WebRootPath).Returns("D:\\kanini training\\C#\\Kanini Tourism\\Kanini Tourism\\wwwroot");

            var controller = new HotelController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = controller.GetHotelById(1);

            // Assert
            var actionResult = Assert.IsType<JsonResult>(result);
            var actualTour = Assert.IsType<Hotels>(actionResult.Value);

            Assert.Equal(expectedTour.HotelId, actualTour.HotelId);
            Assert.Equal(expectedTour.HotelName, actualTour.HotelName);
        
        }

        [Fact]
        public void GetHotelById_NonExistentId_ReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IHotel>();
     
            mockRepository.Setup(repo => repo.GetHotelById(2)).Returns(() => null);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new HotelController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = controller.GetHotelById(2);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

      
        [Fact]
        public async Task CreateHotel_ValidHotel_ReturnsCreatedHotel()
        {
            // Arrange
            var mockRepository = new Mock<IHotel>();
            var expectedHotel = new Hotels
            {
                HotelId = 1,
                HotelName = "Sample Hotel",
                Location = "Sample Location",
                HotelImage = "sample-image.jpg"
            };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.CreateHotel(expectedHotel, mockFormFile.Object)).ReturnsAsync(expectedHotel);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new HotelController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = await controller.Post(expectedHotel, mockFormFile.Object);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result); 
            var actualCreatedHotel = Assert.IsType<Hotels>(createdAtActionResult.Value);
            Assert.Equal(expectedHotel.HotelId, actualCreatedHotel.HotelId);
            Assert.Equal(expectedHotel.HotelName, actualCreatedHotel.HotelName);
          
        }


        [Fact]
        public async Task UpdateHotel_ValidHotel_ReturnsUpdatedHotel()
        {
            // Arrange
            var mockRepository = new Mock<IHotel>();
            var existingHotel = new Hotels
            {
                HotelId = 1,
                HotelName = "Existing Hotel",
                Location = "Existing Location",
                HotelImage = "existing-image.jpg"
            };
            var updatedHotel = new Hotels
            {
                HotelId = 1,
                HotelName = "Updated Hotel",
                Location = "Updated Location",
                HotelImage = "updated-image.jpg"
            };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.UpdateHotel(updatedHotel, mockFormFile.Object)).ReturnsAsync(updatedHotel);
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new HotelController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = await controller.Put(existingHotel.HotelId, updatedHotel, mockFormFile.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Hotels>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualUpdatedHotel = Assert.IsType<Hotels>(okResult.Value);
            Assert.Equal(updatedHotel.HotelId, actualUpdatedHotel.HotelId);
            Assert.Equal(updatedHotel.HotelName, actualUpdatedHotel.HotelName);
         
        }

        [Fact]
        public async Task DeleteHotel_ExistingId_ReturnsDeletedHotelList()
        {
            // Arrange
            var mockRepository = new Mock<IHotel>();
            var hotelToDelete = new Hotels
            {
                HotelId = 1,
                HotelName = "Existing Hotel",
                Location = "Existing Location",
                HotelImage = "existing-image.jpg"
            };
            mockRepository.Setup(repo => repo.DeleteHotelById(1)).ReturnsAsync(new List<Hotels> { hotelToDelete });
            var mockWebHostEnvironment = Mock.Of<IWebHostEnvironment>();
            var controller = new HotelController(mockRepository.Object, mockWebHostEnvironment);

            // Act
            var result = await controller.DeleteHotelById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Hotels>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualDeletedHotelList = Assert.IsType<List<Hotels>>(okResult.Value);
            Assert.Single(actualDeletedHotelList);
            Assert.Equal(hotelToDelete.HotelId, actualDeletedHotelList[0].HotelId);
          
        }

        [Fact]
        public void GetAllHotels_FilterByLocation_ReturnsListOfHotels()
        {
            // Arrange
            var mockRepository = new Mock<IHotel>();
            var expectedHotels = new List<Hotels>
    {
        new Hotels { HotelId = 1, HotelName = "Hotel 1", Location = "Location 1", HotelImage = "accord.jpg" },
        new Hotels { HotelId = 2, HotelName = "Hotel 2", Location = "Location 1", HotelImage = "keralahotel.jpg" }
    };
            mockRepository.Setup(repo => repo.FilterLocation("Location 1")).Returns(expectedHotels.Where(h => h.Location == "Location 1"));

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        
            mockWebHostEnvironment.Setup(env => env.WebRootPath).Returns("D:\\kanini training\\C#\\Kanini Tourism\\Kanini Tourism\\wwwroot");

            var mockFileProvider = new Mock<IFileProvider>();
            mockFileProvider.Setup(provider => provider.GetFileInfo(It.IsAny<string>()))
                            .Returns((string path) => new NotFoundFileInfo(path));
            mockWebHostEnvironment.Setup(env => env.WebRootFileProvider).Returns(mockFileProvider.Object);

            var controller = new HotelController(mockRepository.Object, mockWebHostEnvironment.Object);

            // Act
            var result = controller.GetAllHotels("Location 1");

            // Assert
            var actionResult = Assert.IsType<JsonResult>(result);
            var actualHotels = Assert.IsType<List<Hotels>>(actionResult.Value);

        
            foreach (var hotel in actualHotels)
            {
                Console.WriteLine($"HotelId: {hotel.HotelId}, HotelName: {hotel.HotelName}, Location: {hotel.Location}, HotelImage: {hotel.HotelImage}");
            }

            Assert.Equal(expectedHotels.Count, actualHotels.Count);
   
        }


    }
}
