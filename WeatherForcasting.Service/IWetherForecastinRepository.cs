using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WetherForecasting.Entity;

namespace WeatherForcasting.Service
{
    public interface IWetherForecastinRepository
    {
        Task<List<LocationModel>> ProcessWeatherData(HttpPostedFileBase file);
    }
}
