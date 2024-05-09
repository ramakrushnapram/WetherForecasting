using System.IO;
using System.Threading.Tasks;
using Xunit;
using WetherForecasting.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using WeatherForcasting.Service;
using WetherForecasting.Controllers;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace WetherForecasting.Tests
{
    [TestFixture]
    public class WeatherForecastRepositoryTests
    {
        [Test]
        public void ProcessWeatherData_Test()
        {
            var httpClient = new HttpClient();
            var rep = new Mock<IWetherForecastinRepository>();
            var con = new HomeController(new WeatherForecastRepository());
            rep.
        }

    }
}
