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

namespace Kanini_Tourism.Tests
{
    public class UserTests
    {
        [Fact]
        public async Task GetAllUsers_ReturnsOkResult_WithUsers()
        {
            // Arrange
            var mockRepository = new Mock<IUser>();
            var expectedUsers = new List<User>
            {
                new User { UserId = 1, UserName = "User1", UserEmail = "user1@example.com", Role = "User" },
                new User { UserId = 2, UserName = "User2", UserEmail = "user2@example.com", Role = "User" }
            };
            mockRepository.Setup(repo => repo.GetAllUsers()).ReturnsAsync(expectedUsers);
            var controller = new UserController(mockRepository.Object);

            // Act
            var result = await controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualUsers = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(expectedUsers.Count, actualUsers.Count);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WithJwtToken()
        {
            // Arrange
            var mockRepository = new Mock<IUser>();
            var newUser = new User { UserId = 1, UserName = "NewUser", UserEmail = "newuser@example.com", Role = "User", Password = "password" };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.AddUser(newUser)).ReturnsAsync(newUser);
            var controller = new UserController(mockRepository.Object);

            // Act
            var result = await controller.Register(newUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var jwtToken = Assert.IsType<string>(okResult.Value);
            Assert.NotNull(jwtToken);
        }
    }
}
