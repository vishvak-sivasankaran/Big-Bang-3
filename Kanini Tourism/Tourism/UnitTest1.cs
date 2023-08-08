using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kanini_Tourism.Controllers;
using Kanini_Tourism.Data;
using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Kanini_Tourism.Repository.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Kanini_Tourism.Tests
{
    public class FeedbackServiceTests
    {
        private Mock<IFeedback> _mockFeedbackService;
        private FeedbackController _controller;

        public FeedbackServiceTests()
        {
            _mockFeedbackService = new Mock<IFeedback>();
            _controller = new FeedbackController(_mockFeedbackService.Object);
        }

        [Fact]
        public void Get_ReturnsListOfFeedback()
        {
            // Arrange
            var expectedFeedbacks = new List<Feedback>
        {
            new Feedback { FeedId = 1, Name = "John Doe", Email = "john@example.com", Description = "Good service", Rating = 5 },
            new Feedback { FeedId = 2, Name = "Jane Smith", Email = "jane@example.com", Description = "Excellent tour", Rating = 4 },
        };
            _mockFeedbackService.Setup(repo => repo.GetAllFeedback()).Returns(expectedFeedbacks);

            // Act
            var result = _controller.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Feedback>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualFeedbacks = Assert.IsAssignableFrom<IEnumerable<Feedback>>(okResult.Value);
            Assert.Equal(expectedFeedbacks, actualFeedbacks);
        }

        [Fact]
        public async Task Add_ReturnsListOfFeedbackAfterAdd()
        {
            // Arrange
            var newFeedback = new Feedback { FeedId = 3, Name = "Alice Johnson", Email = "alice@example.com", Description = "Great experience", Rating = 5 };
            var expectedFeedbacks = new List<Feedback>
        {
            new Feedback { FeedId = 1, Name = "John Doe", Email = "john@example.com", Description = "Good service", Rating = 5 },
            new Feedback { FeedId = 2, Name = "Jane Smith", Email = "jane@example.com", Description = "Excellent tour", Rating = 4 },
            newFeedback, // Newly added feedback
        };
            _mockFeedbackService.Setup(repo => repo.AddFeedback(newFeedback)).ReturnsAsync(expectedFeedbacks);

            // Act
            var result = await _controller.Add(newFeedback);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Feedback>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualFeedbacks = Assert.IsAssignableFrom<List<Feedback>>(okResult.Value);
            Assert.Equal(expectedFeedbacks, actualFeedbacks);
        }


        [Fact]
        
        public async Task Update_ExistingFeedback_ReturnsOkObjectResultWithUpdatedFeedback()
        {
            // Arrange
            var feedbackIdToUpdate = 2;
            var updatedFeedback = new Feedback { FeedId = 2, Name = "Jane Smith", Email = "jane@example.com", Description = "Amazing tour", Rating = 5 };

            // Simulate updating the feedback by returning the updated feedback
            _mockFeedbackService.Setup(repo => repo.UpdateFeedback(feedbackIdToUpdate, It.IsAny<Feedback>())).ReturnsAsync(updatedFeedback);

            // Act
            var result = await _controller.Update(feedbackIdToUpdate, updatedFeedback);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var actualFeedback = Assert.IsType<Feedback>(actionResult.Value);

            // Check individual properties to ensure the feedback is updated correctly
            Assert.Equal(updatedFeedback.Name, actualFeedback.Name);
            Assert.Equal(updatedFeedback.Email, actualFeedback.Email);
            Assert.Equal(updatedFeedback.Description, actualFeedback.Description);
            Assert.Equal(updatedFeedback.Rating, actualFeedback.Rating);
        }





        [Fact]
        public async Task DeleteById_ReturnsListOfFeedbackAfterDelete()
        {
            // Arrange
            var feedbackIdToDelete = 1;
            var expectedFeedbacks = new List<Feedback>
        {
            new Feedback { FeedId = 2, Name = "Jane Smith", Email = "jane@example.com", Description = "Excellent tour", Rating = 4 },
        };
            _mockFeedbackService.Setup(repo => repo.DeleteFeedbackById(feedbackIdToDelete)).ReturnsAsync(expectedFeedbacks);

            // Act
            var result = await _controller.DeleteById(feedbackIdToDelete);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Feedback>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualFeedbacks = Assert.IsAssignableFrom<List<Feedback>>(okResult.Value);
            Assert.Equal(expectedFeedbacks, actualFeedbacks);
        }
    }

}
