using WeatherAPI.BusinessLogic.Models;

namespace WeatherAPI.BusinessLogic.Interfaces
{
    public interface IWeatherDataBusinessLogic
    {
        Task<List<WeatherData>> GetWeatherData(DateTime fromDate, DateTime toDate, float lat, float lon);
        Task<List<WeatherDataByDay>> GetWeatherDataByDay(DateTime fromDate, DateTime toDate, float lat, float lon);
    }
}
