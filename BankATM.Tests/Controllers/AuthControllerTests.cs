using BankATM.Common.DTO;
using BankATM.Common.Response;
using BankATM.Controllers;
using BankATM.Service.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace BankATM.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Auth_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var request = new LoginRequestDTO
            {
                Username = "admin",
                Password = "123456"
            };

            var randomToken = "random-jwt-token";
            _mockAuthService.Setup(s => s.AuthenticateAsync(request)).ReturnsAsync(randomToken);

            var result = await _controller.Auth(request);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var response = okResult.Value as ApiResponse<string>;
            response.Should().NotBeNull();

            response.Success.Should().BeTrue();
            response.Data.Should().Be(randomToken);
        }

        [Fact]
        public async Task Auth_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            var request = new LoginRequestDTO
            {
                Username = "Christian",
                Password = "654321"
            };

            _mockAuthService.Setup(s => s.AuthenticateAsync(request)).ReturnsAsync((string)null);

            var result = await _controller.Auth(request);

            result.Should().BeOfType<UnauthorizedResult>();
        }
    }
}
