using Xunit;
using Moq;
using Kanini_Tourism.Models;
using Kanini_Tourism.Controllers;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanini_Tourism.Tests
{
    public class FeedbackTests
    {
        private readonly Mock<IFeedback> _feedbackRepositoryMock;
        private readonly FeedbackController _feedbackController;

        public FeedbackTests()
        {
            _feedbackRepositoryMock = new Mock<IFeedback>();
            _feedbackController = new FeedbackController(_feedbackRepositoryMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnListOfFeedbacks()
        {
            // Arrange
            var feedbacks = new List<Feedback>
            {
                new Feedback { FeedId = 1, Name = "John Doe", Email = "john@example.com", Rating = 5, Description = "Great experience!", UserId = 1 },
                new Feedback { FeedId = 2, Name = "Jane Smith", Email = "jane@example.com", Rating = 4, Description = "Enjoyed the trip.", UserId = 2 }
            };
            _feedbackRepositoryMock.Setup(repo => repo.GetAllFeedback()).Returns(feedbacks);

            // Act
            var result = _feedbackController.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var feedbackList = okResult.Value as IEnumerable<Feedback>;
            Assert.Equal(2, feedbackList.Count());
        }

       

        [Fact]
        public async Task Add_ShouldReturnOkResultWithAddedFeedback()
        {
            // Arrange
            var newFeedback = new Feedback { Name = "John Doe", Email = "john@example.com", Rating = 4, Description = "Nice trip!", UserId = 1 };
            _feedbackRepositoryMock.Setup(repo => repo.AddFeedback(newFeedback)).ReturnsAsync(new List<Feedback> { newFeedback });

            // Act
            var result = await _feedbackController.Add(newFeedback);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var feedbackList = okResult.Value as List<Feedback>;
            Assert.Single(feedbackList);
            Assert.Equal("John Doe", feedbackList[0].Name);
            Assert.Equal(4, feedbackList[0].Rating);
        }

        [Fact]
        public async Task Update_ValidId_ReturnsOkResult()
        {
            // Arrange
            int feedbackId = 1;
            var existingFeedback = new Feedback
            {
                FeedId = feedbackId,
                Name = "John Doe",
                Email = "john@example.com",
                Description = "Great experience!",
                Rating = 5
            };
            var updatedFeedback = new Feedback
            {
                FeedId = feedbackId,
                Name = "Jane Smith",
                Email = "jane@example.com",
                Description = "Awesome trip!",
                Rating = 4
            };

            var mockFeedbackService = new Mock<IFeedback>();
            mockFeedbackService.Setup(service => service.UpdateFeedback(feedbackId, updatedFeedback))
                .ReturnsAsync(existingFeedback);

            var controller = new FeedbackController(mockFeedbackService.Object);

            // Act
            var result = await controller.Update(feedbackId, updatedFeedback);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(existingFeedback, okResult.Value);
        }

        [Fact]
        public async Task Update_InvalidId_ReturnsNotFound()
        {
            // Arrange
            int feedbackId = 1;
            var updatedFeedback = new Feedback
            {
                FeedId = feedbackId,
                Name = "Jane Smith",
                Email = "jane@example.com",
                Description = "Awesome trip!",
                Rating = 4
            };

            var mockFeedbackService = new Mock<IFeedback>();
            mockFeedbackService.Setup(service => service.UpdateFeedback(feedbackId, updatedFeedback))
                .ReturnsAsync((Feedback)null);

            var controller = new FeedbackController(mockFeedbackService.Object);

            // Act
            var result = await controller.Update(feedbackId, updatedFeedback);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task DeleteById_ShouldReturnOkResultWithRemainingFeedbacks()
        {
            // Arrange
            int feedbackIdToDelete = 1;

            var remainingFeedbacks = new List<Feedback>
            {
                new Feedback { FeedId = 2, Name = "Jane Smith", Email = "jane@example.com", Rating = 4, Description = "Enjoyed the trip.", UserId = 2 }
            };

            _feedbackRepositoryMock.Setup(repo => repo.DeleteFeedbackById(feedbackIdToDelete)).ReturnsAsync(remainingFeedbacks);

            // Act
            var result = await _feedbackController.DeleteById(feedbackIdToDelete);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsType<List<Feedback>>(okResult.Value);
            var feedbackList = okResult.Value as List<Feedback>;
            Assert.Single(feedbackList);
            Assert.Equal("Jane Smith", feedbackList[0].Name);
            Assert.Equal(4, feedbackList[0].Rating);
        }


    }
}
