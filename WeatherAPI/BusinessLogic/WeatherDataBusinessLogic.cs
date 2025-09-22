
using AutoMapper;
using WeatherAPI.BusinessLogic.Interfaces;
using WeatherAPI.BusinessLogic.Models;
using Repo = WeatherAPI.Repository.Interfaces;

namespace WeatherAPI.BusinessLogic
{
    public class WeatherDataBusinessLogic : IWeatherDataBusinessLogic
    {
        private readonly Repo.IWeatherDataRepository _weatherDataRepository;
        private readonly IMapper _autoMapper;

        public WeatherDataBusinessLogic(Repo.IWeatherDataRepository weatherDataRepository, IMapper autoMapper)
        {
            _weatherDataRepository = weatherDataRepository;
            _autoMapper = autoMapper;
        }

        public Task<List<WeatherData>?> GetWeatherData()
        {
            var weatherDataP = _weatherDataRepository.GetWeatherData();
            var weatherDataD = _autoMapper.Map<Task<List<WeatherData>>>(weatherDataP);
            //var weatherDataByDay = MapWeatherDataByDay(weatherData.Result);

            return weatherDataD;
        }

        public async Task<List<WeatherDataByDay>> GetWeatherDataByDay()
        {
            var weatherDataP = await _weatherDataRepository.GetWeatherData();

            var weatherDataD = _autoMapper.Map<List<WeatherData>>(weatherDataP);
            var weatherDataByDay = MapWeatherDataByDay(weatherDataD);

            return weatherDataByDay;
        }

        public List<WeatherDataByDay> MapWeatherDataByDay(List<WeatherData>? weatherData)
        {
            var weatherDataMap = new List<WeatherDataByDay>();

            if (weatherData == null) return weatherDataMap;

            foreach (var weatherType in weatherData)
            {

                foreach (var values in weatherType.Values)
                {
                    var exists = weatherDataMap.Find(x => x.Day == values.Day);
                    var weather = weatherDataMap.Find(x => x.Day == values.Day) ?? new WeatherDataByDay();

                    weather.Val = values.Val;
                    weather.Day = values.Day;
                    weather = MapDescription(weather, weatherType.WeatherType);
                    
                    
                   if(exists == null)
                    {
                        weatherDataMap.Add(weather);
                    }
                    
                }

            }

            return weatherDataMap;
        }

        private WeatherDataByDay MapDescription(WeatherDataByDay weather, string weatherType)
        {

            switch (weatherType)
            {
                case "T2M":
                    weather.DailyTemp = weather.Val;
                    break;
                case "T2M_MIN":
                    weather.DailyTempMin = weather.Val;
                    break;
                case "T2M_MAX":
                    weather.DailyTempMax = weather.Val;
                    break;
                default:
                    break;
            }
            return weather;
        }
    }
}