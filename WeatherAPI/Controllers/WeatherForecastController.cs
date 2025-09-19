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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Business.IWeatherDataBusinessLogic _weatherDataBusinessLogic;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, Business.IWeatherDataBusinessLogic weatherDataBusinessLogic)
        {
            _logger = logger;
            _weatherDataBusinessLogic = weatherDataBusinessLogic;
        }

        [HttpGet]
        [Route("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
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
