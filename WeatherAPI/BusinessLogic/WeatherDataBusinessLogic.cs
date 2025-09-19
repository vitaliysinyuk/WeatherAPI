using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WeatherAPI.BusinessLogic.Interfaces;
using WeatherAPI.Repository.Models;
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

        public Task<List<WeatherData>?> GetWeatherData()
        {
            var weatherData = _weatherDataRepository.GetWeatherData();

            var weatherDataByDay = MapWeatherDataByDay(weatherData.Result);

            return weatherData;
        }

        public async Task<List<WeatherDataByDay>> GetWeatherDataByDay()
        {
            var weatherData = await _weatherDataRepository.GetWeatherData();

            var weatherDataByDay = MapWeatherDataByDay(weatherData);

            return weatherDataByDay;
        }

        public List<WeatherDataByDay> MapWeatherDataByDay(List<WeatherData> weatherData)
        {
            var weatherDataMap = new List<WeatherDataByDay>();

            foreach (var weatherType in weatherData)
            {

                foreach (var values in weatherType.Values)
                {
                    var exists = weatherDataMap.Find(x => x.Day == values.Day);
                    var weather = weatherDataMap.Find(x => x.Day == values.Day) ?? new WeatherDataByDay();
                    //var weather = new WeatherDataByDay();
                    weather.WeatherType = weatherType.WeatherType;
                    weather.Val = values.Val;
                    weather.Day = values.Day;
                    weather = MapDescription(weather);
                    
                    

                    
                   // weather.Val = values.Val;
                   if(exists == null)
                    {
                        weatherDataMap.Add(weather);
                    }
                    
                }

            }

            return weatherDataMap;
        }

        private WeatherDataByDay MapDescription(WeatherDataByDay weather)
        {
            //var description = "";
            switch (weather.WeatherType)
            {
                case "T2M":
                    weather.Description = "Daily Average";
                    weather.DailyTemp = weather.Val;
                    break;
                case "T2M_MIN":
                    weather.Description = "Daily Average Min";
                    weather.DailyTempMin = weather.Val;
                    break;
                case "T2M_MAX":
                    weather.Description = "Daily Average Max";
                    weather.DailyTempMax = weather.Val;
                    break;
                default:
                    break;
            }
            return weather;
        }
    }
}