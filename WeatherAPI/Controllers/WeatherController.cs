using Microsoft.AspNetCore.Mvc;
using Business = WeatherAPI.BusinessLogic.Interfaces;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherController : ControllerBase
    {

        private readonly ILogger<WeatherController> _logger;
        private readonly Business.IWeatherDataBusinessLogic _weatherDataBusinessLogic;
        public WeatherController(ILogger<WeatherController> logger, Business.IWeatherDataBusinessLogic weatherDataBusinessLogic)
        {
            _logger = logger;
            _weatherDataBusinessLogic = weatherDataBusinessLogic;
        }



        [HttpGet]
        [Route("GetWeatherData")]
        public  async Task<ActionResult> GetWeatherData(DateTime fromDate, DateTime toDate, float lat, float lon)
        {
            var weatherData = await _weatherDataBusinessLogic.GetWeatherDataByDay(fromDate, toDate, lat, lon);

            if (!weatherData.Any())
            {
                _logger.LogWarning("No weather data returned");
            }
            
            return Ok(weatherData);
        }

    }
}
