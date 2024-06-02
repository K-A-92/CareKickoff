using CareApi.Services;

namespace CareApi.Tests.Services
{
    public class HashingServiceTests
    {
        [Fact]
        public void HashWithSecretKey_GivenNonNullInput_ReturnsNonNullString()
        {
            // Arrange
            var hashingService = new HashingService();

            // Act
            var result = hashingService.HashWithSecretKey("TestInput");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }
    }
}