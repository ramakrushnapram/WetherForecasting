using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WetherForecasting.Entity;
using WetherForecasting.Repositery;

namespace WetherForecasting.Utility
{
    public class WeatherForecastRepository : IWetherForecastinRepository
    {
        public async Task<List<LocationModel>> ProcessWeatherData(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0)
            {
                throw new ArgumentException("No file uploaded or invalid file");
            }

            List<LocationModel> locations = new List<LocationModel>();

            using (var reader = new StreamReader(file.InputStream))
            {
                reader.ReadLine(); // Skip header if present

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    if (values.Length < 5)
                    {
                        throw new ArgumentException("Missing values in CSV");
                    }

                    if (!double.TryParse(values[0], out double latitude) || !double.TryParse(values[1], out double longitude))
                    {
                        throw new ArgumentException("Invalid latitude or longitude format");
                    }

                    LocationModel location = new LocationModel
                    {
                        Latitude = latitude,
                        Longitude = longitude,
                        Timezone = values[2],
                        ForecastDays = int.Parse(values[3]),
                        DailyForecasts = new List<LocationModel.DailyForecast>()
                    };

                    locations.Add(location);
                }
            }

            using (HttpClient client = new HttpClient())
            {
                foreach (var location in locations)
                {
                    string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={location.Latitude}&longitude={location.Longitude}&daily=temperature_2m_max&timezone={location.Timezone}&forecast_days={location.ForecastDays}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    response.EnsureSuccessStatusCode(); // Throws HttpRequestException for non-success status codes

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic forecastData = JsonConvert.DeserializeObject(jsonResponse);

                    foreach (var forecast in forecastData.daily.time)
                    {
                        location.DailyForecasts.Add(new LocationModel.DailyForecast
                        {
                            Date = forecast,
                            Temperature2mMax = forecastData.daily.temperature_2m_max[forecastData.daily.time.IndexOf(forecast)]
                        });
                    }
                }
            }

            return locations;
        }

    }
}