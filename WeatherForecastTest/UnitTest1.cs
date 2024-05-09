using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WeatherForcasting.Service;
using WetherForecasting.Controllers;
using WetherForecasting.Entity;
using WetherForecasting.Utility;

namespace Tests
{
    public class Tests
    {
        private Mock<IWetherForecastinRepository> rep;
        private HomeController con;
        List<LocationModel> res = new List<LocationModel>();
        [SetUp]
        public void Setup()
        {
            rep = new Mock<IWetherForecastinRepository>();
            con = new HomeController(new WeatherForecastRepository());
        }

        [Test]
        public async Task ProcessWeatherData_WithFile_Test()
        {
            string filePath = @"D:\PracticeCsharp\WetherForecasting\WeatherForecastTest\Assets\Weather.csv";
            string contentType = "text/csv";
            byte[] fileBytes = File.ReadAllBytes(filePath);
            MemoryStream memoryStream = new MemoryStream(fileBytes);
            CustomHttpPostedFile customHttpPostedFile = new CustomHttpPostedFile(Path.GetFileName(filePath), contentType, memoryStream);

            rep.Setup(x => x.ProcessWeatherData(customHttpPostedFile)).ReturnsAsync(res);
            var result = await con.UploadFile(customHttpPostedFile);
            Assert.IsNotNull(result);
        }
        [Test]
        public void ProcessWeatherData_WithOutFile_Test()
        {
            rep.Setup(x => x.ProcessWeatherData(null)).ReturnsAsync(res);
            Assert.ThrowsAsync<ArgumentException>(async () => await con.UploadFile(null));
        }
        public class CustomHttpPostedFile : HttpPostedFileBase
        {
            private readonly string _fileName;
            private readonly string _contentType;
            private readonly Stream _inputStream;

            public CustomHttpPostedFile(string fileName, string contentType, Stream inputStream)
            {
                _fileName = fileName;
                _contentType = contentType;
                _inputStream = inputStream;
            }

            public override string FileName => _fileName;

            public override string ContentType => _contentType;

            public override int ContentLength => (int)_inputStream.Length;

            public override Stream InputStream => _inputStream;

            public override void SaveAs(string filename)
            {
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    _inputStream.CopyTo(fileStream);
                }
            }
        }

    }
}