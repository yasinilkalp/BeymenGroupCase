using BeymenGroupCase.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace BeymenGroupCase.ConfigurationTest
{
    public class ConfigurationReaderTests : BaseTest
    {

        // 1. Geçerli bir anahtar için doğru değeri döndürme
        [Fact]
        public async Task GetValue_Should_Return_Correct_Value_For_Valid_Key()
        {
            // Arrange
            var applicationName = "ServiceA";
            var key = "SiteName";
            var expectedValue = "Beymen.com.tr";
             
            var configurationReader = new ConfigurationReader(applicationName, mockDatabase.Object, cacheMock.Object);

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

            
            mockDatabase.Setup(d => d.StringGetAsync(key, CommandFlags.None)).ReturnsAsync(new RedisValue(null));

            var configurationReader = new ConfigurationReader(applicationName, mockDatabase.Object, cacheMock.Object);

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
             
            mockDatabase.Setup(d => d.StringGetAsync(key, CommandFlags.None)).ReturnsAsync("Invalid JSON");

            var configurationReader = new ConfigurationReader(applicationName, mockDatabase.Object, cacheMock.Object);

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
             
            mockDatabase.Setup(d => d.StringGetAsync(intKey, CommandFlags.None)).ReturnsAsync("{\"Name\":\"IntValue\",\"Type\":\"Int\",\"Value\":\"24\",\"IsActive\":true,\"ApplicationName\":\"ServiceA\"}");
            mockDatabase.Setup(d => d.StringGetAsync(boolKey, CommandFlags.None)).ReturnsAsync("{\"Name\":\"BoolValue\",\"Type\":\"Boolean\",\"Value\":\"True\",\"IsActive\":true,\"ApplicationName\":\"ServiceA\"}");

            var configurationReader = new ConfigurationReader(applicationName, mockDatabase.Object, cacheMock.Object);

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
             
            mockDatabase.Setup(d => d.StringGetAsync(keyA, CommandFlags.None)).ReturnsAsync("{\"Name\":\"KeyA\",\"Type\":\"String\",\"Value\":\"Value for ServiceA\",\"IsActive\":true,\"ApplicationName\":\"ServiceA\"}");
            mockDatabase.Setup(d => d.StringGetAsync(keyB, CommandFlags.None)).ReturnsAsync("{\"Name\":\"KeyB\",\"Type\":\"String\",\"Value\":\"Value for ServiceB\",\"IsActive\":true,\"ApplicationName\":\"ServiceB\"}");

            var configurationReaderA = new ConfigurationReader(applicationNameA, mockDatabase.Object, cacheMock.Object);
            var configurationReaderB = new ConfigurationReader(applicationNameB, mockDatabase.Object, cacheMock.Object);

            // Act
            var resultA = await configurationReaderA.GetValue<string>(keyA);
            var resultB = await configurationReaderB.GetValue<string>(keyB);

            // Assert
            Assert.Equal(expectedValueA, resultA);
            Assert.Equal(expectedValueB, resultB);
        }


        [Fact]
        public async Task GetValue_ShouldReturnCachedValue_WhenValueExistsInMemoryCache()
        {
            // Arrange
            string applicationName = "ServiceA";
            string key = "SiteName";
            string expectedValue = "boyner.com.tr";

            // Redis'e erişimde oluşacak bir hata
            RedisException redisException = new RedisException("Redis is unavailable.");

            mockDatabase.Setup(d => d.StringGetAsync(key, CommandFlags.None)).ThrowsAsync(redisException);

            // ConfigurationReader nesnesini oluşturma
            var configurationReader = new ConfigurationReader(applicationName, mockDatabase.Object, cacheMock.Object);

            // Act
            var result = await configurationReader.GetValue<string>(key);

            // Assert
            // Cache'de varolan değerin döndüğünü doğrulama
            Assert.Equal(expectedValue, result);
        } 
    }
}
