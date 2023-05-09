using BeymenCase.Application.Interfaces;
using BeymenCase.Application.Models;
using Xunit;

namespace BeymenCase.Test
{
    public class AppDataTest
    {
        private readonly IConfigurationService _configurationService;
        public AppDataTest(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        [Fact]
        public async void Add()
        {
            var fakeData = new AppDataRequest
            {
                Name = "test",
                ApplicationName = "BeymenCase",
                Type = "string",
                Value = "deneme"
            };

            await _configurationService.SaveAsync(fakeData);

            var actual = await _configurationService.GetByNameAsync(fakeData.Name);
            Assert.Equal(fakeData.Name, actual.Value);
        }

        [InlineData("SiteName", "boyner.com.tr")]
        [Theory]
        public async void GetByName(string name, string expectedValue)
        {
            var actual = await _configurationService.GetByNameAsync(name);
            Assert.Equal(expectedValue, actual.Value);
        }

    }
}
