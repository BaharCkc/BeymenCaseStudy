using BeymenCase.Library;
using System.Threading.Tasks;
using Xunit;

namespace BeymenCase.Test
{
    public class ConfigurationTest
    {
        [InlineData("BeymenCase", "boyner.com.tr")]
        [Theory]
        public async Task Should_Get_Expected_SiteName(string applicationName, string expectedValue)
        {
            var configReader = new ConfigurationReader(applicationName, "Server=.;Database=BeymenCaseDb;Trusted_Connection=True;", 5000);

            var value = await configReader.GetValue<string>("SiteName");

            Assert.Equal(expectedValue, value);
        }
    }
}
