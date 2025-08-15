using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.BusinessLogic.Interfaces
{
    public interface IWeatherDataBusinessLogic
    {
        Task<string> GetWeatherData();
    }
}
