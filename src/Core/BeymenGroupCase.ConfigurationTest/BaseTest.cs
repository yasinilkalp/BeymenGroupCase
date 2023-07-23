using BeymenGroupCase.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StackExchange.Redis;
using System;
using System.Text.Json;

namespace BeymenGroupCase.ConfigurationTest
{
    public class BaseTest
    {
        public static Mock<IDatabase> mockDatabase;
        public static Mock<IMemoryCache> cacheMock;
        private delegate bool TryGetValueCallback(string key, out ConfigurationModel value);

        public BaseTest()
        {
            mockDatabase = new Mock<IDatabase>();

            mockDatabase.Setup(d => d.StringGetAsync(new RedisKey("SiteName"), CommandFlags.None)).ReturnsAsync("{\"Name\":\"SiteName\",\"Type\":\"String\",\"Value\":\"Beymen.com.tr\",\"IsActive\":true,\"ApplicationName\":\"ServiceA\"}");

            string key = "ServiceA.SiteName";
            RedisResult redisValues = RedisResult.Create(new RedisValue[] { new RedisValue(key) });
            mockDatabase.Setup(d => d.ExecuteAsync("KEYS", "*")).ReturnsAsync(redisValues);

            cacheMock = new Mock<IMemoryCache>();

            ConfigurationModel model = new()
            {
                ApplicationName = "ServiceA",
                IsActive = true,
                Name = "SiteName",
                Type = "String",
                Value = "Beymen.com.tr"
            };

            var cacheEntryMock = new Mock<ICacheEntry>();
            cacheMock.Setup(c => c.CreateEntry(It.IsAny<object>()))
                .Returns(cacheEntryMock.Object);

            object? valuePayload = null;
            cacheEntryMock
                .SetupSet(mce => mce.Value = It.IsAny<object>())
                .Callback<object>(v => valuePayload = v);


            // IMemoryCache.TryGet metodu için yapılacak olan davranışı ayarlama
            cacheMock.Setup(c => c.TryGetValue(It.IsAny<string>(), out It.Ref<ConfigurationModel>.IsAny))
                     .Returns(new TryGetValueCallback((string cacheKey, out ConfigurationModel cachedValue) =>
                     {
                         if (cacheKey.ToString() == key)
                         {
                             cachedValue = model;
                             return true;
                         }
                         cachedValue = null;
                         return false;
                     }));



        }

        
    }
}
