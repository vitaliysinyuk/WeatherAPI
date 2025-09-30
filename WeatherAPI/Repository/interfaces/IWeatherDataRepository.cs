using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Repository.Models;

namespace WeatherAPI.Repository.Interfaces
{
    public interface IWeatherDataRepository
    {
        Task<List<WeatherData>?> GetWeatherData(DateTime fromDate, DateTime toDate, float lat, float lon);
    }
}
