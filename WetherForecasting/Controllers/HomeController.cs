using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

using WetherForecasting.Entity;
using WeatherForcasting.Service;

namespace WetherForecasting.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWetherForecastinRepository _weatherForecastRepository;

        public HomeController(IWetherForecastinRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(HttpPostedFileBase file)
        {
            try
            {
                List<LocationModel> locations = await _weatherForecastRepository.ProcessWeatherData(file);
                return View("UploadFile", locations);
            }
            catch (ArgumentException ex)
            {
                throw ;
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View("UploadFile");
        }
    }
}
