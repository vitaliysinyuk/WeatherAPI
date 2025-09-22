using Domain = WeatherAPI.BusinessLogic.Models;
using Persistence = WeatherAPI.Repository.Models;

namespace WeatherAPI.BusinessLogic.Automapper
{
    public class WeatherDataMaps : AutoMapper.Profile
    {
        public WeatherDataMaps() 
        {
            //-- Business --> Database


            //-- Database --> Business
            CreateMap<Persistence.WeatherBase, Domain.WeatherBase>();
            CreateMap<Persistence.WeatherData, Domain.WeatherData>();
            CreateMap<Persistence.WeatherData.Value, Domain.WeatherData.Value>();
            CreateMap<Persistence.WeatherDataByDay, Domain.WeatherDataByDay>();
        }
    }
}
