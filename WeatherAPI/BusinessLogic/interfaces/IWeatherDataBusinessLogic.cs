using System;

using WeatherAPI.BusinessLogic.Models;

namespace WeatherAPI.BusinessLogic.Interfaces
{
    public interface IWeatherDataBusinessLogic
    {
        Task<List<WeatherData>?> GetWeatherData();
        Task<List<WeatherDataByDay>> GetWeatherDataByDay();
    }
}
