using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RouteeFailoverMessages.API.Controllers;
using RouteeFailoverMessages.Domain.Models;
using RouteeFailoverMessages.Library.Services;
using Microsoft.Extensions.Options;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

public class RouteeControllerTests
{
    private readonly Mock<IRouteeService> _routeeServiceMock;
    private readonly Mock<ILogger<RouteeControler>> _loggerMock;
    private readonly RouteeAuth _routeeAuth;
    private readonly RouteeControler _controller;

    public RouteeControllerTests()
    {
        _routeeServiceMock = new Mock<IRouteeService>();
        _loggerMock = new Mock<ILogger<RouteeControler>>();

        var options = Options.Create(new RouteeAuth
        {
            AppID = "test-api-key",
            AppSecret = "test-secret"
        });

        _routeeAuth = options.Value;

        _controller = new RouteeControler(options, _routeeServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetSend_ReturnsOkResult_WithResponse()
    {
        // Arrange
        var request = new Failover_Message_Request();
        var expectedResponse = new Failover_Message_Response
        {
            // Populate with sample data if needed
        };

        _routeeServiceMock
            .Setup(service => service.SendFailoverMessage(request, _routeeAuth))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetSend(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResponse = Assert.IsType<Failover_Message_Response>(okResult.Value);
        Assert.Equal(expectedResponse, actualResponse);

        _routeeServiceMock.Verify(service => service.SendFailoverMessage(request, _routeeAuth), Times.Once);
    }

    [Fact]
    public async Task GetSend_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        var request = new Failover_Message_Request();
        _routeeServiceMock
            .Setup(s => s.SendFailoverMessage(request, It.IsAny<RouteeAuth>()))
            .ThrowsAsync(new System.Exception("Test exception"));

        // Act
        var result = await _controller.GetSend(request);

        // Assert
        var objectResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

        _routeeServiceMock.Verify(service => service.SendFailoverMessage(request, _routeeAuth), Times.Once);
    }
}
