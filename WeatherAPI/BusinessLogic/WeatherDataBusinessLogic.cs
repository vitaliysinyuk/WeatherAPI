using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WeatherAPI.BusinessLogic.Interfaces;
using Repo = WeatherAPI.Repository.Interfaces;

namespace WeatherAPI.BusinessLogic
{
    public class WeatherDataBusinessLogic : IWeatherDataBusinessLogic
    {
        private readonly Repo.IWeatherDataRepository _weatherDataRepository;

        public WeatherDataBusinessLogic(Repo.IWeatherDataRepository weatherDataRepository)
        {
            _weatherDataRepository = weatherDataRepository;
        }

        public Task<string> GetWeatherData()
        {
            var weatherData = _weatherDataRepository.GetWeatherData();

            return weatherData;
        }
    }
}