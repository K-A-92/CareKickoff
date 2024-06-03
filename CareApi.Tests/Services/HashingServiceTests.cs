using CareApi.Services;
using Microsoft.Extensions.Configuration;

namespace CareApi.Tests.Services
{
    public class HashingServiceTests
    {
        [Fact]
        public void HashWithSecretKey_GivenNonNullInput_ReturnsNonNullString()
        {
            // Arrange
            var configuration = Substitute.For<IConfiguration>();
            configuration.GetValue<string>("SECRET_KEY").Returns("TestSecret");
            var hashingService = new HashingService(configuration);

            // Act
            var result = hashingService.HashWithSecretKey("TestInput");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }
    }
}