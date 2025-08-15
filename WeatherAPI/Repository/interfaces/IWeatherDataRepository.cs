using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Repository.Interfaces
{
    public interface IWeatherDataRepository
    {
        Task<string> GetWeatherData();
    }
}
