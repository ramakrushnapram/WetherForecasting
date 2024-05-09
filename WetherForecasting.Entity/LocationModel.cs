using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WetherForecasting.Entity
{
    public class LocationModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Timezone { get; set; }
        public int ForecastDays { get; set; } // Assuming forecast_days is an integer

        // Nested class to represent daily forecast data
        public class DailyForecast
        {
            public string Date { get; set; }
            public double Temperature2mMax { get; set; }
        }

        public List<DailyForecast> DailyForecasts { get; set; } // Collection of daily forecasts
    }
}
