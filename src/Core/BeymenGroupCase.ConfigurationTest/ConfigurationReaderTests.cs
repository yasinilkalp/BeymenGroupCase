using BeymenGroupCase.Configuration;
using Moq;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace BeymenGroupCase.ConfigurationTest
{
    public class ConfigurationReaderTests  
    { 

        // 1. Geçerli bir anahtar için doğru değeri döndürme
        [Fact]
        public async Task GetValue_Should_Return_Correct_Value_For_Valid_Key()
        {
            // Arrange
            var applicationName = "ServiceA";
            var key = "SiteName";
            var expectedValue = "Beymen.com.tr";

            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(d => d.StringGetAsync(key, CommandFlags.None)).ReturnsAsync("{\"Name\":\"SiteName\",\"Type\":\"String\",\"Value\":\"Beymen.com.tr\",\"IsActive\":true,\"ApplicationName\":\"ServiceA\"}");

            var configurationReader = new ConfigurationReader(applicationName, mockDatabase.Object);

            // Act
            var result = await configurationReader.GetValue<string>(key);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        // 2. Geçersiz bir anahtar için null döndürme
        [Fact]
        public async Task GetValue_Should_Return_Null_For_Invalid_Key()
        {
            // Arrange
            var applicationName = "ServiceA";
            var key = "InvalidKey";

            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(d => d.StringGetAsync(key, CommandFlags.None)).ReturnsAsync(new RedisValue(null));

            var configurationReader = new ConfigurationReader(applicationName, mockDatabase.Object);

            // Act
            var result = await configurationReader.GetValue<string>(key);

            // Assert
            Assert.Null(result);
        }

        // 3. JSON ayrıştırma hatası durumu
        [Fact]
        public async Task GetValue_Should_Handle_JsonParsing_Error()
        {
            // Arrange
            var applicationName = "ServiceA";
            var key = "InvalidJsonKey";

            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(d => d.StringGetAsync(key, CommandFlags.None)).ReturnsAsync("Invalid JSON");

            var configurationReader = new ConfigurationReader(applicationName, mockDatabase.Object);
              
            // Act & Assert
            await Assert.ThrowsAsync<JsonException>(async () => await configurationReader.GetValue<string>(key));
        }

        // 4. Doğru türde veri döndürme
        [Fact]
        public async Task GetValue_Should_Return_Correct_Data_Types()
        {
            // Arrange
            var applicationName = "ServiceA";
            var intKey = "IntValue";
            var boolKey = "BoolValue";

            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(d => d.StringGetAsync(intKey, CommandFlags.None)).ReturnsAsync("{\"Name\":\"IntValue\",\"Type\":\"Int\",\"Value\":\"24\",\"IsActive\":true,\"ApplicationName\":\"ServiceA\"}");
            mockDatabase.Setup(d => d.StringGetAsync(boolKey, CommandFlags.None)).ReturnsAsync("{\"Name\":\"BoolValue\",\"Type\":\"Boolean\",\"Value\":\"True\",\"IsActive\":true,\"ApplicationName\":\"ServiceA\"}");

            var configurationReader = new ConfigurationReader(applicationName, mockDatabase.Object);

            // Act
            var intValue = await configurationReader.GetValue<int>(intKey);
            var boolValue = await configurationReader.GetValue<bool>(boolKey);

            // Assert
            Assert.Equal(24, intValue);
            Assert.True(boolValue);
        }

        // 5. Farklı uygulama adları için doğru sonuçlar
        [Fact]
        public async Task GetValue_Should_Return_Correct_Values_For_Different_ApplicationNames()
        {
            // Arrange
            var applicationNameA = "ServiceA";
            var applicationNameB = "ServiceB";
            var keyA = "KeyA";
            var keyB = "KeyB";
            var expectedValueA = "Value for ServiceA";
            var expectedValueB = "Value for ServiceB";

            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup(d => d.StringGetAsync(keyA, CommandFlags.None)).ReturnsAsync("{\"Name\":\"KeyA\",\"Type\":\"String\",\"Value\":\"Value for ServiceA\",\"IsActive\":true,\"ApplicationName\":\"ServiceA\"}");
            mockDatabase.Setup(d => d.StringGetAsync(keyB, CommandFlags.None)).ReturnsAsync("{\"Name\":\"KeyB\",\"Type\":\"String\",\"Value\":\"Value for ServiceB\",\"IsActive\":true,\"ApplicationName\":\"ServiceB\"}");

            var configurationReaderA = new ConfigurationReader(applicationNameA, mockDatabase.Object);
            var configurationReaderB = new ConfigurationReader(applicationNameB, mockDatabase.Object);

            // Act
            var resultA = await configurationReaderA.GetValue<string>(keyA);
            var resultB = await configurationReaderB.GetValue<string>(keyB);

            // Assert
            Assert.Equal(expectedValueA, resultA);
            Assert.Equal(expectedValueB, resultB);
        }

    }
}
