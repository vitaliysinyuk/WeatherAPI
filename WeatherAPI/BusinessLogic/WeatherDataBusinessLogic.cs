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

        public Task<List<WeatherData>> GetWeatherData(DateTime fromDate, DateTime toDate, float lat, float lon)
        {
            var weatherDataP = _weatherDataRepository.GetWeatherData(fromDate, toDate, lat, lon);
            var weatherDataD = _autoMapper.Map<Task<List<WeatherData>>>(weatherDataP);

            return weatherDataD;
        }

        public async Task<List<WeatherDataByDay>> GetWeatherDataByDay(DateTime fromDate, DateTime toDate, float lat, float lon)
        {
            var weatherDataP = await _weatherDataRepository.GetWeatherData(fromDate, toDate, lat, lon);

            var weatherDataD = _autoMapper.Map<List<WeatherData>>(weatherDataP);
            var weatherDataByDay = MapWeatherDataByDay(weatherDataD);

            return weatherDataByDay;
        }

        /// <summary>
        /// Build weather data map by day for front end ReCharts
        /// </summary>
        /// <param name="weatherData"></param>
        /// <returns></returns>
        private List<WeatherDataByDay> MapWeatherDataByDay(List<WeatherData>? weatherData)
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

        /// <summary>
        /// Map values for each specific weather data type 
        /// </summary>
        /// <param name="weather"></param>
        /// <param name="weatherType"></param>
        /// <returns></returns>
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
                case "PRECTOTCORR":
                    weather.Precipitation = weather.Val;
                    break;
                default:
                    break;
            }
            return weather;
        }
    }
}