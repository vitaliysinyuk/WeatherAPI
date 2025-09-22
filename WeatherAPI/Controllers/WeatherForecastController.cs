using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Collections;
using WeatherAPI.Repository.Models;
using Business = WeatherAPI.BusinessLogic.Interfaces;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Business.IWeatherDataBusinessLogic _weatherDataBusinessLogic;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, Business.IWeatherDataBusinessLogic weatherDataBusinessLogic)
        {
            _logger = logger;
            _weatherDataBusinessLogic = weatherDataBusinessLogic;
        }



        [HttpGet]
        [Route("GetWeatherData")]
        public  async Task<ActionResult> GetWeatherData()
        {
            var weatherData = await _weatherDataBusinessLogic.GetWeatherDataByDay();
            
            return Ok(weatherData);
        }

    }
}
