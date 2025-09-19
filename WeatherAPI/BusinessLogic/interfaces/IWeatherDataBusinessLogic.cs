using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Repository.Models;

namespace WeatherAPI.BusinessLogic.Interfaces
{
    public interface IWeatherDataBusinessLogic
    {
        Task<List<WeatherData>?> GetWeatherData();
        Task<List<WeatherDataByDay>> GetWeatherDataByDay();
    }
}
